using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public HeadUpDisplay hud;
    public UpgradeWindow upgradeWindow;
    public StatsWindow statsWindow;
    public PauseMenu pauseMenu;

    private GuiWindow selectedWindow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TryToggleWindow(pauseMenu);
        if (Input.GetKeyDown(KeyCode.E))
            TryToggleWindow(statsWindow);
        if (Input.GetKeyDown(KeyCode.Q))
            TryToggleWindow(upgradeWindow);
    }

    public void ClearWindow()
    {
        if (selectedWindow != null)
            TryToggleWindow(selectedWindow);
    }

    private void TryToggleWindow(GuiWindow window)
    {
        if (selectedWindow == window)
        {
            window.overlay.SetActive(false);
            selectedWindow = null;
            SetPause(false);
        }
        else if (selectedWindow == null)
        {
            window.overlay.SetActive(true);
            selectedWindow = window;
            SetPause(true);
        }
    }

    public void SetPause(bool pause)
    {
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Quit()
    {
        SetPause(false);
        ClearWindow();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
