using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (selectedWindow != null)
                ClearWindow();
            else
                TryToggleWindow(pauseMenu);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
            TryToggleWindow(statsWindow);
        if (Keyboard.current.qKey.wasPressedThisFrame)
            TryToggleWindow(upgradeWindow);
    }

    public void ClearWindow()
    {
        if (selectedWindow != null)
            TryToggleWindow(selectedWindow);
    }

    public void TryOpenWindow(GuiWindow window)
    {
        if (selectedWindow == null)
        {
            window.overlay.SetActive(true);
            selectedWindow = window;
            SetPause(true);
        }
    }

    public void TryCloseWindow(GuiWindow window)
    {
        if (selectedWindow == window)
        {
            window.overlay.SetActive(false);
            selectedWindow = null;
            SetPause(false);
        }
    }

    public void TryToggleWindow(GuiWindow window)
    {
        if (window == selectedWindow)
            TryCloseWindow(window);
        else
            TryOpenWindow(window);
    }

    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }

    public void Quit()
    {
        SetPause(false);
        ClearWindow();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
