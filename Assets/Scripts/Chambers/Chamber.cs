using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    [SerializeField] public Transform SafeSpawnLocation;
    [SerializeField] public List<Door> Exits = new List<Door>();
    [HideInInspector] public List<Door> Entrances = new List<Door>();
    [HideInInspector] public List<GameObject> Enemies = new List<GameObject>();
    [HideInInspector] public List<GameObject> Bosses = new List<GameObject>();
    public bool IsLastCHamber = false;
    [HideInInspector] public UnityEngine.Events.UnityEvent LastChamberCleared = new UnityEngine.Events.UnityEvent();
    private int remainingEnemies = -1;
    private int remainingBosses = -1;

    private void Start()
    {
        remainingEnemies = Enemies.Count;
        foreach (var enemy in Enemies)
            enemy.GetComponent<Enemy>().KillEvent.AddListener(KillEnemyEventHandler);

        remainingBosses = Bosses.Count;
        foreach (var enemy in Bosses)
            enemy.GetComponent<Enemy>().KillEvent.AddListener(KillBossEventHandler);
    }

    public void OpenExits()
    {
        if (Exits != null)
            foreach (var door in Exits)
                door?.OpenDoor();
    }

    public void CloseExits()
    {
        if (Exits != null)
            foreach (var door in Exits)
                door?.CloseDoor();
    }

    public void OpenEntrances()
    {
        if (Entrances != null)
            foreach (var door in Entrances)
                door?.OpenDoor();
    }

    public void CloseEntrances()
    {
        if (Entrances != null)
            foreach (var door in Entrances)
                door?.CloseDoor();
    }

    public bool isCleared()
    {
        return remainingEnemies <= 0 && remainingBosses <= 0;
    }

    public List<Door> getExits()
    {
        return Exits;
    }

    public List<Door> getEntrances()
    {
        return Entrances;
    }

    public void KillEnemyEventHandler()
    {
        remainingEnemies--;
        if (isCleared())
            OpenExits();
        if (IsLastCHamber)
            LastChamberCleared.Invoke();
    }

    public void KillBossEventHandler()
    {
        remainingBosses--;
        if (isCleared())
            OpenExits();
        if (IsLastCHamber)
            LastChamberCleared.Invoke();
    }
}
