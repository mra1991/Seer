using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Toggle))]


public class SetMotionBlur : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVol; //we need to talk to post process volume to toggle motion blur effect
    private MotionBlur motionBlur;
    private Toggle toggle; 

    // Start is called before the first frame update
    void Start()
    {
        //try to cache the motion blur settings if it exists 
        postProcessVol.sharedProfile.TryGetSettings<MotionBlur>(out motionBlur);

        //PlayerPrefs.SetInt("MotionBlur", 1); //using 1 and 0 like boolean
        toggle = GetComponent<Toggle>();
        bool isMotionBlurOn = (PlayerPrefs.GetInt("MotionBlur", 1) == 1);
        toggle.isOn = isMotionBlurOn;
        motionBlur.active = isMotionBlurOn;
    }

    public void SetMotionBlurEffect(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("MotionBlur", 1); //On
        }
        else
        {
            PlayerPrefs.SetInt("MotionBlur", 0); //Off
        }
        motionBlur.active = isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
