using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MainMenuView mainMenuView;

    private MenuView activeView;

    public void Start()
    {
        mainMenuView = GetComponentInChildren<MainMenuView>();

        SetActiveView(mainMenuView);
    }

    public void SetActiveView(MenuView view)
    {
        if (activeView != null)
            activeView.SetActive(false);
        view.SetActive(true);
    }

    public void StartGame()
    {
        activeView.SetActive(false);
        SceneManager.LoadScene(Scenes.Game);
    }

    public void Quit()
    {
        activeView.SetActive(false);
        Application.Quit();
    }
}
