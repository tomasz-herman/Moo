using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    private bool showConsole, showHelp;
    private Vector2 scroll;
    private string input = "";

    public Dictionary<string, Command> Commands;

    private void Awake()
    {
        ActionCommand help = new ActionCommand(
            "help",
            "Shows all commands",
            "help",
            () => showHelp = true);
        ActionCommand restore = new ActionCommand(
            "restore",
            "Restores health and ammo",
            "restore",
            () =>
            {
                HealthSystem healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
                AmmoSystem ammoSystem = GameObject.Find("Player").GetComponent<AmmoSystem>();
                healthSystem.Health = healthSystem.MaxHealth;
                ammoSystem.Ammo = ammoSystem.MaxAmmo;
            });
        ActionCommand<float> setHealth = new ActionCommand<float>(
            "set-h",
            "Sets health to given value",
            "set-health <health>",
            health =>
            {
                HealthSystem healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
                healthSystem.Health = health;
            });
        ActionCommand<string> godMode = new ActionCommand<string>(
            "gm",
            "Turns on or off god mode",
            "gm <on|off>",
            state =>
            {
                HealthSystem healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
                healthSystem.godMode = state switch
                {
                    "on" => true,
                    "off" => false,
                    _ => healthSystem.godMode
                };
            });
        ActionCommand<string> spawn = new ActionCommand<string>(
            "spawn",
            "Spawns enemy",
            "spawn <small|medium|big|mini-boss|boss>",
            enemyType =>
            {
                Transform playerTransform = GameObject.Find("Player").transform;
                EnemyTypes type = enemyType switch
                {
                    "small" => EnemyTypes.Small,
                    "medium" => EnemyTypes.Medium,
                    "big" => EnemyTypes.Big,
                    "mini-boss" => EnemyTypes.MiniBoss,
                    "boss" => EnemyTypes.Boss,
                    _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, 
                        "Should be one of: small, medium, big, mini-boss, boss")
                };

                EnemyPrefabInfo enemyInfo = Enemys.GetEnemyInfoFromType(type);
                Instantiate(enemyInfo.enemy, playerTransform.position + playerTransform.forward * 5, Quaternion.identity);
            });
        ActionCommand<float> setTimeScale = new ActionCommand<float>(
            "sts", 
            "Sets time scale", 
            "sts <scale>", 
            scale => Time.timeScale = scale);
        ActionCommand<string> endGame = new ActionCommand<string>(
            "end", 
            "Ends the game", 
            "end <win|lose>",
            result =>
            {
                GameWorld gameWorld = GameObject.Find("GameWorld").GetComponent<GameWorld>();
                ScoreSystem scoreSystem = GameObject.Find("Player").GetComponent<ScoreSystem>();
                bool win = result switch
                {
                    "win" => true,
                    "lose" => false,
                    _ => throw new ArgumentOutOfRangeException(nameof(result), result, 
                        "Should be one of: win, lose")
                };
                gameWorld.EndGame(win, scoreSystem.GetScore());
            });
        Commands = new Dictionary<string, Command>
        {
            [help.Id] = help, 
            [setTimeScale.Id] = setTimeScale,
            [restore.Id] = restore,
            [setHealth.Id] = setHealth,
            [spawn.Id] = spawn,
            [godMode.Id] = godMode,
            [endGame.Id] = endGame
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
            showConsole = true;
    }

    private void OnGUI()
    {
        if(!showConsole) return;

        float y = 0;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * Commands.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width, 90), scroll, viewport);

            int i = 0;
            foreach (var pair in Commands)
            {
                Command command = pair.Value;
                string label = $"{command.Format} - {command.Description}";
                GUI.Label(new Rect(5, 20 * i, viewport.width - 100, 20), label);
                i++;
            }
            
            GUI.EndScrollView();
            y += 100;
        }
        
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.SetNextControlName("field");
        input = GUI.TextField(new Rect(10, y + 5, Screen.width - 20, 20), input);
        GUI.FocusControl("field");

        if(Event.current.type == EventType.Used)
            switch (Event.current.keyCode)
            {
                case KeyCode.Escape:
                    input = "";
                    showConsole = false;
                    break;
                case KeyCode.Return:
                    HandleInput();
                    input = "";
                    break;
            }
    }

    private void HandleInput()
    {
        try
        {
            string[] tokens = input.Split(' ');
            Command command = Commands[tokens[0]];
            if (command is ActionCommand voidCommand) voidCommand.Invoke();
            else if (command is ActionCommand<int> intCommand) intCommand.Invoke(int.Parse(tokens[1]));
            else if (command is ActionCommand<float> floatCommand) floatCommand.Invoke(float.Parse(tokens[1]));
            else if (command is ActionCommand<string> stringCommand) stringCommand.Invoke(tokens[1]);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}

public class Command
{
    public string Id { get; }
    public string Description { get; }
    public string Format { get; }

    public Command(string id, string description, string format)
    {
        Id = id;
        Description = description;
        Format = format;
    }
}

public class ActionCommand : Command
{
    private Action action;
    
    public ActionCommand(string id, string description, string format, Action action) : base(id, description, format)
    {
        this.action = action;
    }

    public void Invoke()
    {
        action.Invoke();
    }
}

public class ActionCommand<T> : Command
{
    private Action<T> action;
    
    public ActionCommand(string id, string description, string format, Action<T> action) : base(id, description, format)
    {
        this.action = action;
    }

    public void Invoke(T value)
    {
        action.Invoke(value);
    }
}