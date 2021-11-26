using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    [SerializeField] List<Door> Entrances = new List<Door>();
    [SerializeField] List<Door> Exits = new List<Door>();
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    private int remainingEnemies = -1;

    private void Start()
    {
        remainingEnemies = Enemies.Count;
    }

    public void OpenExits()
    {
        if (Exits != null)
            foreach (var door in Exits)
                door.OpenDoor();
    }

    public void CloseExits()
    {
        if (Exits != null)
            foreach (var door in Exits)
                door.CloseDoor();
    }

    public void OpenEntrances()
    {
        if (Entrances != null)
            foreach (var door in Entrances)
                door.OpenDoor();
    }

    public void CloseEntrances()
    {
        if (Entrances != null)
            foreach (var door in Entrances)
                door.CloseDoor();
    }

    public bool isCleared()
    {
        return remainingEnemies <= 0;
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
    }
}
