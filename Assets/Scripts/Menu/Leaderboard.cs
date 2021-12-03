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

    private LinkedList<LeaderboardEntry> entries = new LinkedList<LeaderboardEntry>();

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

    public void Save()
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            File.WriteAllLines(path, entries.Select(e => e.Serialize()));
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    public IEnumerable<LeaderboardEntry> GetEntries() { return entries; }
}
