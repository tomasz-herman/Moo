using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Leaderboard
{
    private LinkedList<LeaderboardEntry> entries = new LinkedList<LeaderboardEntry>();

    public void LoadFromFile(string path)
    {
        entries.Clear();
        try
        {
            foreach (string line in LineFileHelper.Load(path))
            {
                try
                {
                    LeaderboardEntry entry = new LeaderboardEntry(line);
                    entries.AddLast(entry);
                }
                catch (Exception e2) { Debug.LogError(e2.Message); }
            }
        }
        catch (Exception e1) { Debug.LogError(e1.Message); }

        var sorted = entries.ToList().OrderBy(e => e);
        entries.Clear();
        foreach (var entry in sorted)
            entries.AddLast(entry);
    }

    public void Add(LeaderboardEntry entry)
    {
        if(entries.Count == 0)
        {
            entries.AddLast(entry);
            return;
        }
        LinkedListNode<LeaderboardEntry> cur = entries.First;

        while(true)
        {
            if(entry.CompareTo(cur.Value) < 0)
            {
                entries.AddBefore(cur, entry);
                return;
            }
            if(cur.Next == null)
            {
                entries.AddLast(entry);
                return;
            }
            cur = cur.Next;
        }
    }

    public void SaveToFile(string path)
    {
        try
        {
            LineFileHelper.Save(path, entries.Select(e => e.Serialize()));
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    public IEnumerable<LeaderboardEntry> GetEntries() { return entries.ToList(); }
}
