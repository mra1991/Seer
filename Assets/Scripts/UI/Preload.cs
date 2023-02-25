using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Preload : MonoBehaviour
{
    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        InitValue();
        Scene currentScene = SceneManager.GetActiveScene();
        if (async == null) //making sure a scene is not already loading
        {
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1); //load next scene
            async.allowSceneActivation = false; //don't let the scene activate 'till we know it is done loading
        }
    }

    void InitValue()
    {
        Time.timeScale = 1.0f; //make sure game is not paused
        System.GC.Collect(); //call garbage collector
    }

    // Update is called once per frame
    void Update()
    {
        if (async.progress > 0.89f && SplashScreen.isFinished) //scene (almost) done loading
        {
            async.allowSceneActivation = true; //allow the new scene to be activated
        }
    }
}
