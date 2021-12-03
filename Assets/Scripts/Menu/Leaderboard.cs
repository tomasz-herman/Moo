using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Leaderboard
{
    private string path;
    public Leaderboard(string path)
    {
        this.path = path;
        LoadFromFile(path);
    }

    private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    private void LoadFromFile(string path)
    {
        entries.Clear();
        try
        {
            foreach (string line in File.ReadLines(path))
            {
                try
                {
                    LeaderboardEntry entry = new LeaderboardEntry(line);
                    entries.Add(entry);
                }
                catch (Exception) { }
            }
        }
        catch (Exception) { }
    }

    public void Save()
    {
        try
        {
            File.WriteAllLines(path, entries.Select(e => e.Serialize()));
        }
        catch (Exception) { }
    }
}
