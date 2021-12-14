using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<GameObject> Chambers;
    [SerializeField] List<GameObject> BossChambers;
    [SerializeField] List<GameObject> Corridors;
    [SerializeField] List<GameObject> Enemys;
    [SerializeField] GameObject Boss;
    [SerializeField] GameWorld GameWorld;
    private HashSet<(int x, int y)> taken = new HashSet<(int x, int y)>();
    private System.Random rng = new System.Random();


    void Start()
    {
        var StartChamber = Instantiate(Chambers[Mathf.FloorToInt(Random.Range(0, Chambers.Count))], Vector3.zero, Quaternion.identity).GetComponent<Chamber>();
        taken.Add((0, 0));
        (int x, int y) current = (0, 0);
        var en = Instantiate(Enemys[0], StartChamber.SafeSpawnLocation.position, StartChamber.SafeSpawnLocation.rotation);
        en.GetComponent<Enemy>().deathSummon = null;
        StartChamber.Enemies.Add(en);
        Player.transform.position = StartChamber.transform.position + StartChamber.SafeSpawnLocation.position;
        var exits = StartChamber.getExits();

        CorridorPlacement newCorridorPlacement = null;
        GameObject newRoom = null;
        Chamber newChamber = null;
        var door = exits[FindGoodRandomIndex(exits, current)];
        exits.Clear();
        exits.Add(door);
        for (int i = 0; i < 9; i++)
        {
            current = (current.x + (int)door.transform.forward.x, current.y + (int)door.transform.forward.z);
            newCorridorPlacement = Instantiate(Corridors[Mathf.FloorToInt(Random.Range(0, Corridors.Count))], door.transform.position - door.transform.forward, door.transform.rotation).GetComponent<CorridorPlacement>();
            if (i % 3 == 2)
            {
                newRoom = Instantiate(BossChambers[Mathf.FloorToInt(Random.Range(0, BossChambers.Count))], Vector3.zero, Quaternion.identity);
                taken.Add((current.x, current.y));
                taken.Add((current.x, current.y - 1));
            }
            else
            {
                newRoom = Instantiate(Chambers[Mathf.FloorToInt(Random.Range(0, Chambers.Count))], Vector3.zero, Quaternion.identity);
                taken.Add((current.x, current.y));
            }
            newChamber = newRoom.GetComponent<Chamber>();
            PlaceChamberOnCorridor(newCorridorPlacement, newRoom, newChamber);
            exits = newChamber.Exits;
            int ind = FindGoodRandomIndex(exits, current);
            door = exits[ind];
            RandomAddSideChambers(exits, current, ind);

            //exits.Clear();
            //exits.Add(door);
        }

        exits.Clear();
        newChamber.IsLastCHamber = true;
        newChamber.LastChamberCleared.AddListener(() => { GameWorld.EndGame(true, Player.GetComponent<Player>().scoreSystem.GetScore()); });
    }

    private void PlaceChamberOnCorridor(CorridorPlacement corridorPlacement, GameObject newRoom, Chamber newChamber)
    {
        Door theDoor = null;
        foreach (var door in newChamber.Exits)
        {
            if (door.transform.forward == corridorPlacement.NextDoorTransform.forward)
                theDoor = door;
        }
        Vector3 offset = newRoom.transform.position - theDoor.transform.position;
        newRoom.transform.position = corridorPlacement.NextDoorTransform.position + offset;

        newChamber.Exits.Remove(theDoor);
        newChamber.Entrances.Add(theDoor);
        corridorPlacement.GetComponent<Corridor>().CorridorCenterPassed.AddListener(newChamber.OpenEntrances);

        var en = Instantiate(Enemys[0], newChamber.SafeSpawnLocation.position, newChamber.SafeSpawnLocation.rotation);
        en.GetComponent<Enemy>().deathSummon = null;
        newChamber.Enemies.Add(en);
    }

    private void RandomAddSideChambers(List<Door> exits, (int x, int y) current, int excludedIndex = -1)
    {
        if (Random.value < 0.5f)
            return;

        int ind = FindGoodRandomIndex(exits, current, excludedIndex);
        if (ind != -1)
        {
            var door = exits[ind];
            (int x, int y) tempCurrent = (current.x + (int)door.transform.forward.x, current.y + (int)door.transform.forward.z);
            var newCorridorPlacement = Instantiate(Corridors[Mathf.FloorToInt(Random.Range(0, Corridors.Count))], door.transform.position - door.transform.forward, door.transform.rotation).GetComponent<CorridorPlacement>();
            var newRoom = Instantiate(Chambers[Mathf.FloorToInt(Random.Range(0, Chambers.Count))], Vector3.zero, Quaternion.identity);
            taken.Add((tempCurrent.x, tempCurrent.y));
            var newChamber = newRoom.GetComponent<Chamber>();
            PlaceChamberOnCorridor(newCorridorPlacement, newRoom, newChamber);
            newChamber.Exits.Clear();
        }
    }

    private int FindGoodRandomIndex(List<Door> doors, (int x, int y) current, int excludedIndex = -1)
    {
        foreach (int ind in GetRandomOrder(doors.Count))
        {
            if (!taken.Contains((current.x + (int)doors[ind].transform.forward.x, current.y + (int)doors[ind].transform.forward.z)))
                if (ind == excludedIndex)
                    continue;
                else
                    return ind;
        }
        return -1;
    }

    public int[] GetRandomOrder(int numberOfElements)
    {
        int[] ind = Enumerable.Range(0, numberOfElements).ToArray();

        int[] RandomInd = ind.OrderBy(x => rng.Next()).ToArray();
        return RandomInd;
    }

}
