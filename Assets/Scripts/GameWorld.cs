using System;
using Assets.Scripts.SoundManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public UserInterface userInterface;
    public Timer timer;
    public AudioManager audioManager;
    public BackgroundMusicManager BackgroundMusicManager;

    void Start()
    {
        timer.SetTicking(true);
        BackgroundMusicManager?.Play();
    }

    private void Update()
    {
        //TODO remove when there is no need to debug-trigger game win
        if (!IsPaused() && Keyboard.current.fKey.isPressed)
            EndGame(true, Utils.NumberBetween(100,999));
    }

    public static bool IsPaused() { return Time.timeScale == 0; }

    public void EndGame(bool win, int playerBaseScore)
    {
        //TODO don't forget about proper score calculation later on
        EndGameData data = new EndGameData(playerBaseScore, win, (int)timer.GetElapsedTime().TotalMilliseconds, DateTimeOffset.Now.ToUnixTimeSeconds());
        EndGameView.SetEndGameData(data);
        MainMenu.ScheduleShowEndgameScreen();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
