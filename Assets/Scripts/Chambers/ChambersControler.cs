using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChambersControler : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameWorld gameWorld;

    [HideInInspector] public ChamberNode CurrentChamber = null;

    SpawnScript Spawn;
    ChamberNode ChamberTreeRoot;
    void Start()
    {
        Spawn = GetComponent<SpawnScript>();
        ChamberTreeRoot = Spawn.GenerateTree();
        Spawn.BuildChambersRec(ChamberTreeRoot);
        CurrentChamber = ChamberTreeRoot;
        Player.transform.position = ChamberTreeRoot.ChamberControl.SpawnLocations[0].transform.position;
        TeleporterEffectScript.CreateTeleporterForEntity(Player, Player.GetComponent<Player>().TeleporterScale);
    }

    void Update()
    {
        ChangeChamber();
        CurrentChamber.ChamberControl.ChamberUpdate();
    }

    private bool ChangeChamber()
    {
        Vector2Int playerPosition = new Vector2Int(Mathf.FloorToInt(Player.transform.position.x / (float)Spawn.ChamberSize), Mathf.FloorToInt(Player.transform.position.z / (float)Spawn.ChamberSize));
        if ((playerPosition - CurrentChamber.Location).magnitude > 1)
            return false;
        if (playerPosition != CurrentChamber.Location)
        {
            CurrentChamber = CurrentChamber.GetNextNodeFromDirecion(playerPosition - CurrentChamber.Location);
            if (CurrentChamber.IsLast)
                CurrentChamber.ChamberControl.AddAllEnemiesKilledListener(GameFinishedHandler);
            return true;
        }
        return false;
    }

    private void GameFinishedHandler()
    {
        gameWorld.EndGame(true, Player.GetComponent<ScoreSystem>().IntScore);
    }
}
