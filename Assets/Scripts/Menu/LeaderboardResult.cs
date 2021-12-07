using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardResult : MonoBehaviour
{
    [SerializeField] private TMP_Text position;
    [SerializeField] private TMP_Text result;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text duration;
    [SerializeField] private TMP_Text date;

    [SerializeField] private Color winColor;
    [SerializeField] private Color lossColor;

    public void Load(int id, LeaderboardEntry entry)
    {
        position.text = $"#{id}";
        if(entry.GameData.Won)
        {
            result.color = winColor;
            result.text = "WIN";
        }
        else
        {
            result.color = lossColor;
            result.text = "LOSS";
        }
        playerName.text = entry.PlayerName;
        score.text = entry.GameData.Score.ToString();
        duration.text = entry.GameData.GetElapsedTimeString();
        date.text = DateTimeOffset.FromUnixTimeSeconds(entry.GameData.Timestamp).ToString("dd.MM.yyyy HH:mm");
    }
}
