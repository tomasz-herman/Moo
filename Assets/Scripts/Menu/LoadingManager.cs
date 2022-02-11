using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    private float progress;
    private void Awake()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        
        scenesLoading.Add(SceneManager.LoadSceneAsync(Scenes.TestGeneration, LoadSceneMode.Additive));

        StartCoroutine(LoadScenes());
    }

    private IEnumerator LoadScenes()
    {
        Time.timeScale = 0;
        foreach (var scene in scenesLoading)
        {
            while (!scene.isDone)
            {
                progress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    progress += operation.progress;
                }

                progress /= scenesLoading.Count;
                progressBar.value = progress / 2;
                
                yield return null;
            }
        }

        for (int i = 0; i <= 50; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            progressBar.value = 0.5f + i / 50f;
        }

        loadingScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
