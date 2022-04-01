using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : GuiWindow
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject pausePanel;

    public void SetShowOptions(bool show)
    {
        pausePanel.SetActive(!show);
        options.SetActive(show);
    }

    private void OnEnable()
    {
        SetShowOptions(false);
    }
}
