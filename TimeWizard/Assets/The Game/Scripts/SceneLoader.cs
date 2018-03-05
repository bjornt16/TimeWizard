using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneLoader : SingletonMB<SceneLoader>
{

    private AsyncOperation async;

    public string loadScene;

    public LoadingUI loadingUIPrefab;
    private LoadingUI loadingUIinstance;

    public override void CopyValues(SingletonMB<SceneLoader> copy)
    {
        SceneLoader Copy = copy.GetComponent<SceneLoader>();
        loadingUIPrefab = Copy.loadingUIPrefab != null ? Copy.loadingUIPrefab : loadingUIPrefab;
    }

    IEnumerator LoadScene(string scene)
    {
        if (scene == "")
            yield break;

        async = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        if (async == null)
            yield break;
        //instantiate the loading screen, with progress bar.
        loadingUIinstance = Instantiate(loadingUIPrefab) as LoadingUI;

        //update progress bar as scene loads.
        while (!async.isDone)
        {
            loadingUIinstance.setPercentage(async.progress);
            yield return null;
        }

        loadingUIinstance.Destroy();
        loadingUIinstance = null;

        async = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!async.isDone)
        {
            yield return null;
        }

    }

    public void Load(string scene)
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        StartCoroutine(LoadScene(scene));
    }
}