using Assets.Scripts.SoundManager;
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
    private bool firstTimeLoading = true;

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

        if(firstTimeLoading)
        {
            Config.Load(ApplicationData.configPath);
            Application.quitting += () => Config.Save(ApplicationData.configPath);
            firstTimeLoading = false;
            ConfigEntry entry = Config.Entry;

            AudioManager audio = AudioManager.Instance;
            audio.MusicVolume = entry.musicVolume;
            audio.SoundVolume = entry.soundVolume;
            audio.UiVolume = entry.uiVolume;

            string[] qualityNames = QualitySettings.names;
            for (int i = 0; i < qualityNames.Length; i++)
            {
                if (qualityNames[i] == entry.graphicsQuality)
                {
                    QualitySettings.SetQualityLevel(i);
                    break;
                }
            }
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
        SceneManager.LoadScene(Scenes.LoadingScreen);
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
