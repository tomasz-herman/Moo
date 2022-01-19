using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChambersControler : MonoBehaviour
{
    [SerializeField] GameObject Player;

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
    }

    void Update()
    {
        if(ChangeChamber())
        {

        }
    }

    private bool ChangeChamber()
    {
        Vector2Int playerPosition = new Vector2Int((int)(Player.transform.position.x / (float)Spawn.ChamberSize), (int)(Player.transform.position.z / (float)Spawn.ChamberSize));
        if (playerPosition != CurrentChamber.Location)
        {
            CurrentChamber = CurrentChamber.GetChildFromDirection(playerPosition - CurrentChamber.Location);
            return true;
        }
        return false;
    }
}
