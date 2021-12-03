using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MenuView
{
    [SerializeField] private LeaderboardResult resultPrefab;

    public void OnContinue()
    {
        Menu.ShowMainMenu();
    }
}
