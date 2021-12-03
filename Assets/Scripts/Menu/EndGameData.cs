using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EndGameData
{
    public string PlayerName { get; private set; }
    public int Score { get; private set; }
    public int ElapsedMilliseconds { get; private set; }
    public long Timestamp { get; private set; }
    public bool GameFinished { get; private set; }

    public EndGameData(string name, int score, bool finished, int elapsedMs, long timestamp)
    {
        PlayerName = name;
        Score = score;
        ElapsedMilliseconds = elapsedMs;
        Timestamp = timestamp;
        GameFinished = finished;
    }
}
