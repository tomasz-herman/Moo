using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject overlay;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!overlay.activeSelf);
        }
    }

    public void SetPause(bool pause)
    {
        overlay.SetActive(pause);
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;     
    }

    public void Resume()
    {
        Debug.Log("Hmm");
        SetPause(false);
    }

    public void Quit()
    {
        SetPause(false);
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
