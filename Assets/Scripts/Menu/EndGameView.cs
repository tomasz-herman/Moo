using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MenuView
{
    private static EndGameData endGameData; //needs to be accessed from a different scene
    [SerializeField] private Color winColor, loseColor;
    [SerializeField] private Text resultText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private TMP_InputField nameInput;

    private void Recalculate()
    {
        scoreText.text = endGameData.Score.ToString();
        timeText.text = endGameData.GetElapsedTimeString();
        nameInput.text = string.Empty;
        if(endGameData.Won)
        {
            resultText.text = "You won!";
            resultText.color = winColor;
        }
        else
        {
            resultText.text = "You died!";
            resultText.color = loseColor;
        }
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

    public void OnContinue()
    {
        LeaderboardView view = Menu.GetLeaderboardView();
        Leaderboard leaderboard = view.GetLeaderboard();
        leaderboard.Add(new LeaderboardEntry(nameInput.text, endGameData));
        view.SaveLeaderboard();

        Menu.ShowLeaderboard();
    }
}
