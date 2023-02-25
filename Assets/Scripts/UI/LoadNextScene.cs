using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private AsyncOperation async;

    public void LoadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.seerDamage.FullHeal(); //heal the player to restore vignette effect
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        if (async == null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
            async.allowSceneActivation = true;
        }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
