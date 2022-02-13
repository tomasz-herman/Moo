using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public Sprite[] backgrounds;
    public Image backgroundImage;
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    private float progress;
    public float secondsToWait;
    private void Awake()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        loadingScreen.gameObject.SetActive(true);
        
        scenesLoading.Add(SceneManager.LoadSceneAsync(Scenes.TestGeneration, LoadSceneMode.Additive));

        StartCoroutine(LoadScenes());
    }

    private IEnumerator LoadScenes()
    {
        int scenes = scenesLoading.Count;
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

                progress /= scenes + 1;
                progressBar.value = progress;
                
                yield return null;
            }
        }

        const float waitTimeTick = 0.1f;
        int waitingTicks = Mathf.CeilToInt(secondsToWait / waitTimeTick);
        for (int i = 0; i < waitingTicks; i++)
        {
            progress = (scenes + (i + 1.0f) / waitingTicks) / (scenes + 1.0f);
            progressBar.value = progress;
            yield return new WaitForSecondsRealtime(waitTimeTick);
        }

        loadingScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
