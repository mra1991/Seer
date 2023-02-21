using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ActivateChannel : MonoBehaviour
{

    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParam;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioM.SetFloat(nameParam, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioM.SetFloat(nameParam, -80f);
        }
    }

}
