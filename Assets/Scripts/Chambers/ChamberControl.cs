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
    private EnemySpawner enemySpawner;
    public ChamberNode node;
    private Dictionary<Direction, SegmentControler> segments = new Dictionary<Direction, SegmentControler>();

    private void Awake()
    {
        SpawnLocations = GetComponentsInChildren<SpawnLocationScript>().ToList();
        symbol = gameObject.GetComponentInChildren<PathSymbolControler>();
        fightTrigger = gameObject.GetComponentInChildren<FightTrigger>();
        foreach (var item in gameObject.GetComponentsInChildren<SegmentControler>())
        {
            segments.Add(item.direction, item);
        }
        enemySpawner = new EnemySpawner(GameWorld);
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
                Fight();
                return;
            case States.Cleared:
            default:
                return;
        }
    }

    protected virtual void PreFight()
    {
        if (Physics.CheckSphere(fightTrigger.transform.position, fightTrigger.TriggerRadius, LayerMask.GetMask(Layers.Player)))
        {
            State = States.Fight;
            SetFightPathsColors();
            Enemy boss = enemySpawner.Spawn(SpawnLocations, node);
            if (boss != null)
                GameWorld.userInterface.hud.bossBar.TrackedEnemy = boss;

            BackgroundMusicManager.SwapBackgroundMusicPlaying(GameWorld.FightMusicManager, GameWorld.IdleMusicManager);
        }
    }

    protected virtual void Fight()
    {
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

    protected enum States { PreFight, Fight, Cleared }

    public bool IsCleared()
    {
        if (State == States.Cleared)
            return true;
        return false;
    }

    public void SetBlocadeActive(Direction direction, bool isActive)
    {
        if (segments.Count > 0)
            segments[direction].SetActive(isActive);
    }

    private void AllEnemiesKilledHandler()
    {
        SetDefaultPathsColors();
        BackgroundMusicManager.SwapBackgroundMusicPlaying(GameWorld.IdleMusicManager, GameWorld.FightMusicManager);
        State = States.Cleared;
    }

    public void AddAllEnemiesKilledListener(UnityEngine.Events.UnityAction action)
    {
        enemySpawner.AllEnemiesKilled.AddListener(action);
    }
}
