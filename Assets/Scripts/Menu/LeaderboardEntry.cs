using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct LeaderboardEntry: IComparable<LeaderboardEntry>
{
    public EndGameData GameData { get; private set; }
    public string PlayerName { get; private set; }

    public LeaderboardEntry(string name, EndGameData data)
    {
        GameData = data;
        PlayerName = name;
    }

    public int CompareTo(LeaderboardEntry other)
    {
        int cmpScore = GameData.Score.CompareTo(other.GameData.Score);
        if (cmpScore != 0)
            return -cmpScore;
        return GameData.Timestamp.CompareTo(other.GameData.Timestamp);
    }
}
