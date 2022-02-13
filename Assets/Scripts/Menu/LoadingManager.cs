using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        loadingScreen.gameObject.SetActive(true);
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(loadingScreen);
    }

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        scenesLoading.Add(SceneManager.LoadSceneAsync(Scenes.TestGeneration, LoadSceneMode.Single));

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
        Destroy(loadingScreen);
        Destroy(gameObject);
    }

}
