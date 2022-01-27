using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChambersControler : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnEffect;
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
        Player.gameObject.SetActive(false);
        var tele = Instantiate(SpawnEffect, Player.transform.position, Quaternion.identity).GetComponent<TeleporterEffectScript>();
        tele.gameObject.transform.localScale = new Vector3(2, 2, 2);
        tele.AddSpawnedObject(Player);
    }

    void Update()
    {
        ChangeChamber();
        CurrentChamber.ChamberControl.ChamberUpdate();
        if (EnemySpawner.MaxNumber == CurrentChamber.Number)
            if (CurrentChamber.ChamberControl.IsCleared())
                gameWorld.EndGame(true, Player.GetComponent<ScoreSystem>().GetScore());
    }

    private bool ChangeChamber()
    {
        Vector2Int playerPosition = new Vector2Int(Mathf.FloorToInt(Player.transform.position.x / (float)Spawn.ChamberSize), Mathf.FloorToInt(Player.transform.position.z / (float)Spawn.ChamberSize));
        if (playerPosition != CurrentChamber.Location)
        {
            CurrentChamber = CurrentChamber.GetNextNodeFromDirecion(playerPosition - CurrentChamber.Location);
            return true;
        }
        return false;
    }
}
