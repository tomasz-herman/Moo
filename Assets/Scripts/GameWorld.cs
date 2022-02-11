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
    public Player player;
    public ChambersControler chambersControler;

    void Start()
    {
        timer.SetTicking(true);
        IdleMusicManager?.Play();

        audioManager = AudioManager.Instance;
        player = FindObjectOfType<Player>();
        chambersControler = FindObjectOfType<ChambersControler>();
    }

    public bool IsPaused() { return Time.timeScale == 0; }

    public void EndGame(bool win)
    {
        float score = player.GetComponent<ScoreSystem>().Score;
        double timeMs = timer.GetElapsedTime().TotalMilliseconds;
        if(win)
        {
            //TODO: we need a nicer API for getting these values - preferably put it in GameplayConfig scriptable object - best place for it
            var spawnScript = chambersControler.GetComponent<SpawnScript>();
            var gameplay = ApplicationData.GameplayData;
            int normalChambersBeforeBoss = (int)typeof(SpawnScript).GetField("NumbersOfChambersBeforeBoss").GetValue(spawnScript);
            int optionalChambersBeforeBoss = (int)typeof(SpawnScript).GetField("NumberOfOptionalChambersBeforeBoss").GetValue(spawnScript);
            int bossChambers = (int)typeof(SpawnScript).GetField("NumberOfBossChambers").GetValue(spawnScript);

            float expectedTimeToClearAllChambers = 0f;
            float expectedTimeToClearObligatoryChambers = 0f;

            int chamberCount = chambersControler.ChamberCount;
            for(int i = 0; i < chamberCount; i++)
            {
                var node = chambersControler.GetChamberNode(i);
                float expectedClearTime = node.Type switch
                {
                    ChamberType.Normal => gameplay.NormalChamberClearTime,
                    ChamberType.Boss => gameplay.NormalChamberClearTime,
                    ChamberType.Optional => gameplay.NormalChamberClearTime,
                    _ => 0,
                };

                expectedClearTime *= gameplay.GetChamberClearTimeScalingMultiplier(node.Level);

                expectedTimeToClearAllChambers += expectedClearTime;
                if (node.Type == ChamberType.Normal || node.Type == ChamberType.Boss)
                    expectedTimeToClearObligatoryChambers += expectedClearTime;
            }

            //for time to clear all we receive 0 bonus, for time to clear obligatory we receive bonus, we interpolate linearly everything else, clamped to min 0
            //f(x) = ax + b
            //f(all) = 0            => a * all + b = 0
            //f(obligatory) = 1     => a * obl + b = 1
            float a = 1 / (expectedTimeToClearObligatoryChambers - expectedTimeToClearAllChambers);
            float b = - a * expectedTimeToClearAllChambers;
            float bonusMultiplier = Mathf.Max(0, a * (float)timeMs / 1000 + b);

            score += bonusMultiplier * gameplay.QuickWinScoreBonus;
        }


        EndGameData data = new EndGameData(Mathf.CeilToInt(score), win, (int)timeMs, DateTimeOffset.Now.ToUnixTimeSeconds());
        EndGameView.SetEndGameData(data);
        MainMenu.ScheduleShowEndgameScreen();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
