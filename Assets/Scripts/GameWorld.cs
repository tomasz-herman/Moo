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

    void Start()
    {
        timer.SetTicking(true);
        IdleMusicManager?.Play();
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (!Debug.isDebugBuild)
        {
            if (!Application.isFocused && !IsPaused())
            {
                if (userInterface.selectedWindow != null)
                    userInterface.ClearWindow();
                else
                    userInterface.TryToggleWindow(userInterface.pauseMenu);
            }
        }
    }

    public bool IsPaused() { return Time.timeScale == 0; }

    public void EndGame(bool win, int playerBaseScore)
    {
        //TODO: don't forget about proper score calculation later on
        EndGameData data = new EndGameData(playerBaseScore, win, (int)timer.GetElapsedTime().TotalMilliseconds, DateTimeOffset.Now.ToUnixTimeSeconds());
        EndGameView.SetEndGameData(data);
        MainMenu.ScheduleShowEndgameScreen();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
