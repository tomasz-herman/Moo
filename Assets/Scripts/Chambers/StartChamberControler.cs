using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChamberControler : ChamberControl
{
    protected override void PreFight()
    {
        SetDefaultPathsColors();
        State = States.Cleared;
    }

    protected override void Fight()
    {
    }
}
