using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public GameObject loadingScreen;
    public Image progressBar;
    public Image background;
    public AudioSource gameMusic;
    public List<Sprite> backgroundSprites = new List<Sprite>();
    public float minMusicVolume, maxMusicVolume;

    public void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)ScenesHolder.SPLASHSCENE, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(int unloadScene, int loadScene, int notSplashScreen) // 0 means it's the splash screen, 1 for all other scenes
    {
        if(notSplashScreen == 1)
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(FadeOutMusic(1));

        Sprite chosenSprite = null;
            if (PlayerPrefs.GetInt("LevelToLoad") == 0)
                chosenSprite = backgroundSprites[0];
            else if (PlayerPrefs.GetInt("LevelToLoad") == 2)
                chosenSprite = backgroundSprites[1];
            if (PlayerPrefs.GetInt("LevelToLoad") == 5)
                chosenSprite = backgroundSprites[2];

            background.sprite = chosenSprite;

            
        
        scenesLoading.Add(SceneManager.UnloadSceneAsync(unloadScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(loadScene));
        
    }



    float totalSceneProgress;
    IEnumerator GetSceneLoadProgress (int loadScene)
    {
        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count);

                progressBar.fillAmount = totalSceneProgress;

                yield return null;
            }
        }

        loadingScreen.SetActive(false);

        scenesLoading.Clear();

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(loadScene));
    }

    public IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = gameMusic.volume;

        while (gameMusic.volume > minMusicVolume)
        {
            gameMusic.volume -= (startVolume - minMusicVolume) * Time.deltaTime / fadeTime;

            yield return null;
        }

        gameMusic.volume = minMusicVolume;
    }

    public void FadeIn ()
    {
        StartCoroutine(FadeInMusic(5));
    }

    public IEnumerator FadeInMusic(float fadeTime)
    {
        float startVolume = gameMusic.volume;       
        while (gameMusic.volume < maxMusicVolume)
        {
            gameMusic.volume += (maxMusicVolume - startVolume) * Time.deltaTime / fadeTime;

            yield return null;
        }

        gameMusic.volume = maxMusicVolume;
    }

    public void changeMusic (AudioClip audClip)
    {
        gameMusic.clip = audClip;
    }
}
