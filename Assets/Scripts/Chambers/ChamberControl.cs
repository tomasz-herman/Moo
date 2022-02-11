using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SoundManager;
using UnityEngine;

public class ChamberControl : Entity
{
    [HideInInspector] public List<SpawnLocationScript> SpawnLocations;
    [HideInInspector] public PathSymbolControler symbol;
    protected States State = States.PreFight;
    private FightTrigger fightTrigger;
    private EnemySpawner enemySpawner = new EnemySpawner();
    public ChamberNode node;
    private Dictionary<Direction, SegmentControler> segments = new Dictionary<Direction, SegmentControler>();
    private List<Door> doors = null;
    private UnityEngine.Events.UnityEvent DoorOpenEvent = new UnityEngine.Events.UnityEvent();

    private void Awake()
    {
        SpawnLocations = GetComponentsInChildren<SpawnLocationScript>().ToList();
        symbol = gameObject.GetComponentInChildren<PathSymbolControler>();
        fightTrigger = gameObject.GetComponentInChildren<FightTrigger>();
        doors = gameObject.GetComponentsInChildren<Door>().ToList();
        foreach (var item in doors)
            item.MoveStopEvent.AddListener(MoveOffHandler);
        foreach (var item in gameObject.GetComponentsInChildren<SegmentControler>())
            segments.Add(item.direction, item);
    }

    private void Start()
    {
        AddAllEnemiesKilledListener(AllEnemiesKilledHandler);
    }

    private void OnDestroy()
    {
        enemySpawner.AllEnemiesKilled.RemoveListener(AllEnemiesKilledHandler);
    }

    public void ChamberUpdate()
    {
        switch (State)
        {
            case States.PreFight:
                PreFight();
                return;
            case States.Fight:
            case States.DoorsClosing:
            case States.DoorsOpening:
            case States.Cleared:
            default:
                return;
        }
    }

    protected virtual void PreFight()
    {
        if (Physics.CheckSphere(fightTrigger.transform.position, fightTrigger.TriggerRadius, LayerMask.GetMask(Layers.Player)))
        {
            State = States.DoorsClosing;
            SetDoors(false);
        }
    }

    protected virtual void Fight()
    {
    }

    protected virtual void DoorsClosing()
    {
        bool closed = true;
        if (doors != null)
            foreach (var item in doors)
            {
                if (!item.IsClosed())
                {
                    closed = false;
                    break;
                }
            }
        if (closed)
        {
            State = States.Fight;
            SetFightPathsColors();
            Enemy boss = enemySpawner.Spawn(SpawnLocations, node);
            if (boss != null)
                GameWorld.userInterface.hud.bossBar.TrackedEnemy = boss;

            BackgroundMusicManager.SwapBackgroundMusicPlaying(GameWorld.FightMusicManager, GameWorld.IdleMusicManager);
        }
    }

    protected virtual void DoorsOpening()
    {
        bool opened = true;
        if (doors != null)
            foreach (var item in doors)
            {
                if (!item.IsOpen())
                {
                    opened = false;
                    break;
                }
            }
        if (opened)
        {
            SetDefaultPathsColors();
            BackgroundMusicManager.SwapBackgroundMusicPlaying(GameWorld.IdleMusicManager, GameWorld.FightMusicManager);
            State = States.Cleared;
            DoorOpenEvent.Invoke();
        }
    }

    public void SetFightPathsColors()
    {
        Material fightMaterial = PathMaterials.GetMaterialFromType(PathTypes.Fight);
        symbol.SetMaterials(fightMaterial);
        foreach (var item in segments)
        {
            item.Value.SetPathMaterial(fightMaterial);
        }
    }
    public void SetDefaultPathsColors()
    {
        symbol.SetDefaultMaterials();
        foreach (var item in segments)
        {
            item.Value.SetPathMaterial(symbol.materials[item.Key]);
        }
    }
    public void SetNonActivePathsColors()
    {
        Material noneMaterial = PathMaterials.GetMaterialFromType(PathTypes.None);
        symbol.SetMaterials(noneMaterial);
        foreach (var item in segments)
        {
            item.Value.SetPathMaterial(noneMaterial);
        }
    }

    public void SetClearedPathColor(Direction direction)
    {
        if (segments.ContainsKey(direction))
        {
            symbol.materials[direction] = PathMaterials.GetMaterialFromType(PathTypes.Cleared);
        }
    }

    protected enum States { PreFight, Fight, Cleared, DoorsClosing, DoorsOpening }

    public bool IsCleared()
    {
        if (State == States.Cleared)
            return true;
        return false;
    }

    public void SetBlocadeActive(Direction direction, bool isActive)
    {
        if (segments.Count > 0)
            segments[direction].SetPathBlocade(isActive);

        foreach (var item in doors.ToList())
        {
            if (!item.gameObject.activeInHierarchy)
                doors.Remove(item);
        }
    }

    private void AllEnemiesKilledHandler()
    {
        State = States.DoorsOpening;
        SetDoors(true);
    }

    public void AddAllEnemiesKilledListener(UnityEngine.Events.UnityAction action)
    {
        enemySpawner.AllEnemiesKilled.AddListener(action);
    }

    public void AddDoorsOpenListener(UnityEngine.Events.UnityAction action)
    {
        DoorOpenEvent.AddListener(action);
    }

    private void SetDoors(bool isOpen)
    {
        if (doors != null)
            foreach (var item in doors)
            {
                if (isOpen)
                    item.OpenDoor();
                else
                    item.CloseDoor();
            }
    }

    private void MoveOffHandler(bool isClosed)
    {
        if (isClosed)
        {
            if (State == States.DoorsClosing)
                DoorsClosing();
        }
        else
        {
            if (State == States.DoorsOpening)
                DoorsOpening();
        }
    }
}
