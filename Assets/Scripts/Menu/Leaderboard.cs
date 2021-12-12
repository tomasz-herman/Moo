using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Leaderboard
{
    private LinkedList<LeaderboardEntry> entries = new LinkedList<LeaderboardEntry>();

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

    public IEnumerable<LeaderboardEntry> GetEntries() { return entries.ToList(); }
}
