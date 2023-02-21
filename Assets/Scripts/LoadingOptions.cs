using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class LoadingOptions : MonoBehaviour
{
    /// <summary>
    /// Modes of use:
    /// 1) Preloader, don't put anything in the variables
    /// 2) with progressbar or percentage: Assign progressbar and/or txtPercent
    /// 3) WaitForUserInput: can also be used with a cutscene but don't use the delay
    /// 4) Delay: When you want to display hints. but don't use WaitForUserInput
    /// X) if you don't use loadSceneByName or loadSceneByIndex the next scene will load
    /// X1) you can load a specific scene by name using loadSceneByNumber
    /// X2) you can load a specific scene by its index using loadSceneByIndex ( if equal to -1 it means next scene)
    /// </summary>
    private AsyncOperation async;
    [SerializeField] private Image progressbar;
    [SerializeField] private Text txtPercent;
    [SerializeField] private bool waitForUserInput = false;
    private bool ready = false;
    [SerializeField] private GameObject waitForUserInputTxt;
    [SerializeField] private float delay = 0f;
    [SerializeField] private string loadSceneByName = "";
    [SerializeField] private int loadSceneByIndex = -1;

    private bool anyKey = false;

    // Start is called before the first frame update
    void Start()
    {
        InitValue();
        if (loadSceneByName != "") //loading scene by its name
        {
            async = SceneManager.LoadSceneAsync(loadSceneByName);
        }
        else if (loadSceneByIndex < 0) //loading next scene (invalid buildIndex)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        else //loading scene by its index
        {
            async = SceneManager.LoadSceneAsync(loadSceneByIndex);
        }
        async.allowSceneActivation = false;
        if (!waitForUserInput)
        {
            Invoke("Activate", delay);
        }
        if (waitForUserInputTxt)
        {
            waitForUserInputTxt.SetActive(false);
        }
    }

    public void OnAnyKey(InputAction.CallbackContext context)
    {
        anyKey = context.performed;
    }

    public void Activate()
    {
        ready = true;
    }

    void InitValue()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();
    }

    // Update is called once per frame
    void Update()
    {
        if (progressbar)
        {
            progressbar.fillAmount = async.progress + 0.1f;
        }

        if (txtPercent)
        {
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("F2") + " %";
        }

        if (waitForUserInput && waitForUserInputTxt && async.progress > 0.89f)
        {
            waitForUserInputTxt.SetActive(true);
        }

        if (waitForUserInput && anyKey)
        {
            if (async.progress > 0.89f && SplashScreen.isFinished)
            {
                ready = true;
            }

        }

        if (async.progress > 0.89f && SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;
        }
    }
}
