using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : MenuView
{
    [SerializeField] private LeaderboardResult resultPrefab;

    private Leaderboard leaderboard = null;
    private GameObject listObject;
    
    protected override void Awake()
    {
        base.Awake();
        listObject = GetComponentInChildren<VerticalLayoutGroup>().gameObject;
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);
        if(active)
        {
            TryLoadLeaderboard();
            Recalculate();
        }
    }

    private void Recalculate()
    {
        foreach (var entry in listObject.GetComponentsInChildren<LeaderboardResult>())
            Destroy(entry.gameObject);

        TryLoadLeaderboard();

        int i = 1;
        foreach(var entry in leaderboard.GetEntries())
        {
            LeaderboardResult result = Instantiate(resultPrefab);
            result.transform.SetParent(listObject.transform);
            result.Load(i++, entry);
            result.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void TryLoadLeaderboard()
    {
        if (leaderboard == null)
        {
            leaderboard = new Leaderboard();
            var entries = LeaderboardParser.LoadFromFile(ApplicationData.leaderboardPath);
            if(entries != null)
            {
                foreach (var entry in entries)
                    leaderboard.Add(entry);
            }
        }
    }

    public void SaveLeaderboard()
    {
        if(leaderboard != null)
        {
            LeaderboardParser.SaveToFile(ApplicationData.leaderboardPath, leaderboard.GetEntries());
        }
    }

    public Leaderboard GetLeaderboard()
    {
        TryLoadLeaderboard();
        return leaderboard;
    }

    public void OnContinue()
    {
        Menu.ShowMainMenu();
    }
}
