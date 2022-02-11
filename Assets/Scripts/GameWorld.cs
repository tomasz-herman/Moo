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

    private void Start()
    {
        timer.SetTicking(true);
        IdleMusicManager?.Play();
        audioManager = AudioManager.Instance;
        player = FindObjectOfType<Player>();
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
        float score = player.GetComponent<ScoreSystem>().Score;
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
