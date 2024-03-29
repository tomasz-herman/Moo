using System;
using Assets.Scripts.SoundManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public UserInterface userInterface;
    public Timer timer;
    public AudioManager audioManager;
    public BackgroundMusicManager FightMusicManager;
    public BackgroundMusicManager IdleMusicManager;
    public ScoreSystem ScoreSystem;
    public ChambersControler chambersControler;

    private void Start()
    {
        timer.SetTicking(true);
        IdleMusicManager?.Play();
        audioManager = AudioManager.Instance;
        chambersControler = FindObjectOfType<ChambersControler>();
    }

    private void Update()
    {
        if (Debug.isDebugBuild) return;
        if (Application.isFocused || IsPaused()) return;

        if (userInterface.selectedWindow != null)
            userInterface.ClearWindow();
        else
            userInterface.TryToggleWindow(userInterface.pauseMenu);
    }

    public bool IsPaused() { return Time.timeScale == 0; }
    public void EndGame(bool win)
    {
        userInterface.endScreen.Open(win);
        userInterface.enabled = false;
    }
    public void ReturnToMenu(bool win)
    {
        float score = ScoreSystem.Score;
        double timeMs = timer.GetElapsedTime().TotalMilliseconds;
        if(win)
        {
            var gameplay = ApplicationData.GameplayData;

            float expectedTimeToClearAllChambers = 0f;
            float expectedTimeToClearObligatoryChambers = 0f;

            int chamberCount = chambersControler.ChamberCount;
            for(int i = 0; i < chamberCount; i++)
            {
                var node = chambersControler.GetChamberNode(i);
                float expectedClearTime = node.Type switch
                {
                    ChamberType.Normal => gameplay.NormalChamberClearTime,
                    ChamberType.Boss => gameplay.BossChamberClearTime,
                    ChamberType.Optional => gameplay.OptionalChamberClearTime,
                    _ => 0,
                };

                expectedClearTime *= gameplay.GetChamberClearTimeScalingMultiplier(node.Level);

                expectedTimeToClearAllChambers += expectedClearTime;
                if (node.Type == ChamberType.Normal || node.Type == ChamberType.Boss)
                    expectedTimeToClearObligatoryChambers += expectedClearTime;
            }

            float bonusMultiplier = 1 - ((float)timeMs / 1000 - expectedTimeToClearObligatoryChambers)/(expectedTimeToClearAllChambers - expectedTimeToClearObligatoryChambers);
            bonusMultiplier = Mathf.Max(0, bonusMultiplier);

            float expectedScoreInOptionalChambers = 0;
            for (int i = 0; i < chamberCount; i++)
            {
                var node = chambersControler.GetChamberNode(i);
                if (node.Type == ChamberType.Optional)
                    expectedScoreInOptionalChambers += ApplicationData.SpawnData.enemiesSpawnData.GetExpectedEnemyScoreForChamber(node);
            }

            score += bonusMultiplier * expectedScoreInOptionalChambers * gameplay.QuickWinScoreFactor;
        }


        EndGameData data = new EndGameData(Mathf.CeilToInt(score), win, (int)timeMs, DateTimeOffset.Now.ToUnixTimeSeconds());
        EndGameView.SetEndGameData(data);
        MainMenu.ScheduleShowEndgameScreen();
        SceneManager.LoadScene(Scenes.MainMenu);
    }

    public Enemy SpawnEnemy(EnemyTypes type, Vector3 position, int level)
    {
        EnemyPrefabInfo enemyinfo = Enemys.GetEnemyInfoFromType(type);
        var enemy = GameObject.Instantiate(enemyinfo.enemy, position, Quaternion.identity);
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.Level = level;
        enemyClass.Spawn();
        TeleporterEffectScript.CreateTeleporterForEntity(enemy, enemyinfo.teleporterScale);
        return enemyClass;
    }
}
