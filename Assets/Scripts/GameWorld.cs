using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public UserInterface userInterface;
    public Timer timer;

    void Start()
    {
        timer.SetTicking(true);
    }

    public bool IsPaused() { return Time.timeScale == 0; }
}
