using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameView : MenuView
{
    private static EndGameData endGameData; //needs to be accessed from a different scene
    

    private void Recalculate()
    {

    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);
        Recalculate();
    }

    public static void SetEndGameData(EndGameData data)
    {
        endGameData = data;
    }
}
