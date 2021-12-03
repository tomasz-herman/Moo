using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public UserInterface userInterface;
    public Timer timer;

    void Start()
    {
        timer.SetTicking(true);
    }

    public bool IsPaused() { return Time.timeScale == 0; }

    public void EndGame(EndGameData data)
    {
        EndGameView.SetEndGameData(data);
        SceneManager.LoadScene(Scenes.MainMenu);
        //TODO think of how to show death screen from here
    }
}
