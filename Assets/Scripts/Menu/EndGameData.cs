using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EndGameData
{
    public int Score { get; private set; }
    public int ElapsedMilliseconds { get; private set; }
    public long Timestamp { get; private set; }
    public bool Won { get; private set; }

    public EndGameData(int score, bool won, int elapsedMs, long timestamp)
    {
        Score = score;
        ElapsedMilliseconds = elapsedMs;
        Timestamp = timestamp;
        Won = won;
    }

    public string GetElapsedTimeString()
    {
        return new TimeSpan(0, 0, 0, 0, ElapsedMilliseconds).ToString(@"h\:mm\:ss\.fff");
    }
}
