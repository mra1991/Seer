using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGamePrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("HeroPosX", 300f);
        PlayerPrefs.SetFloat("HeroPosY", 30.65f);
        PlayerPrefs.SetFloat("HeroPosZ", 181f);
        PlayerPrefs.SetFloat("HeroRotX", 0f);
        PlayerPrefs.SetFloat("HeroRotY", 45f);
        PlayerPrefs.SetFloat("HeroRotZ", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
