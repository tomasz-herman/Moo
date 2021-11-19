using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEntryList : MonoBehaviour
{
    public StatEntry entryPrefab;
    public void AddEntry(string name, string value)
    {
        StatEntry newEntry = Instantiate(entryPrefab);
        newEntry.transform.SetParent(gameObject.transform, false);
        newEntry.SetName(name);
        newEntry.SetValue(value);
    }

    public void Clear()
    {
        foreach (var entry in GetComponentsInChildren<StatEntry>())
            Destroy(entry.gameObject);
    }
}
