using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LeaderboardParser
{
    public static IEnumerable<LeaderboardEntry> LoadFromFile(string path)
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        try
        {
            foreach (string line in LineFileHelper.Load(path))
            {
                try
                {
                    LeaderboardEntry entry = Parse(line);
                    entries.Add(entry);
                }
                catch (Exception e2) { Debug.LogError(e2.Message); }
            }
        }
        catch (Exception e1)
        { 
            Debug.LogError(e1.Message);
            return null;
        }

        return entries;
    }

    public static void SaveToFile(string path, IEnumerable<LeaderboardEntry> entries)
    {
        try
        {
            LineFileHelper.Save(path, entries.Select(e => Serialize(e)));
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    private static string Serialize(LeaderboardEntry entry)
    {
        EndGameData data = entry.GameData;
        return $"{entry.PlayerName};{data.Score};{data.ElapsedMilliseconds};{data.Timestamp};{data.Won}";
    }

    private static LeaderboardEntry Parse(string text)
    {
        string[] split = text.Split(';');

        string name = split[0];
        int score = int.Parse(split[1]);
        int elapsedMilliseconds = int.Parse(split[2]);
        long timestamp = long.Parse(split[3]);
        bool won = bool.Parse(split[4]);

        EndGameData data = new EndGameData(score, won, elapsedMilliseconds, timestamp);

        return new LeaderboardEntry(name, data);
    }
}
