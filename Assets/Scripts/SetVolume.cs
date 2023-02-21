using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM = null;
    [SerializeField] private string nameParam = null;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetFloat(nameParam, 0.3f);

        slider = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParam, 0.3f); //+0db (full volume)
        slider.value = v;
        audioM.SetFloat(nameParam, Mathf.Log10(v) * 30);
    }

    public void SetVol(float vol)
    {
        audioM.SetFloat(nameParam, Mathf.Log10(vol) * 30);
        PlayerPrefs.SetFloat(nameParam, vol);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
