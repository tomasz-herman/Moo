using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool showEndgameScreen = false;
    private MainMenuView mainMenuView;
    private EndGameView endGameView;
    private LeaderboardView leaderboardView;
    private OptionsView optionsView;

    private MenuView activeView;

    public void Start()
    {
        mainMenuView = GetComponentInChildren<MainMenuView>(true);
        endGameView = GetComponentInChildren<EndGameView>(true);
        leaderboardView = GetComponentInChildren<LeaderboardView>(true);
        optionsView = GetComponentInChildren<OptionsView>(true);

        SetActiveView(mainMenuView);

        if (showEndgameScreen)
        {
            SetActiveView(endGameView);
            showEndgameScreen = false;
        } 
    }

    public void SetActiveView(MenuView view)
    {
        if (activeView != null)
            activeView.SetActive(false);
        
        view.SetActive(true);
        activeView = view;
    }

    public void StartGame()
    {
        activeView.SetActive(false);
        SceneManager.LoadScene(Scenes.TestGeneration);
    }

    public void Quit()
    {
        activeView.SetActive(false);
        Application.Quit();
    }

    public void ShowLeaderboard() { SetActiveView(leaderboardView); }
    public void ShowOptions() { SetActiveView(optionsView); }
    public void ShowMainMenu() { SetActiveView(mainMenuView); }
    public LeaderboardView GetLeaderboardView() { return leaderboardView; }

    public static void ScheduleShowEndgameScreen() { showEndgameScreen = true; }
}
