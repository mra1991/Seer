using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SeerDamage : MonoBehaviour
{
    [Tooltip("Maximum Health Points.")][SerializeField] private int maxHP = 10;
    [Tooltip("After how long the player can take damage agin.")] 
    [SerializeField] private float damageDuration = 1f; //after how long the player can take damage again
    [SerializeField] private float healEvery = 2f;
    private float timeSinceLastHeal = 0f;
    private int hp; //current health points
    private bool takingDamage = false;

    //variables for increasing vignette post process effect
    [SerializeField] private PostProcessVolume postProcessVol; //we need to talk to post process volume to increase vignette when player takes damage
    private Vignette vignette;
    [Range(0.1f,0.5f)][SerializeField] private float initialVignetteIntensity = 0.35f;
    [Range(0f, 0.05f)] [SerializeField] private float increaseIntensityBy = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP; //make sure to start with maximum health points
        //try to cache the vignette settings if they exist 
        postProcessVol.sharedProfile.TryGetSettings<Vignette>(out vignette);
        vignette.intensity.value = initialVignetteIntensity; //set the intensity to initial value
    }

    // Update is called once per frame
    void Update()
    {
        if (!takingDamage) //can't heal while taking damage
        {
            if (timeSinceLastHeal < healEvery)
            {
                timeSinceLastHeal += Time.deltaTime;
            }
            else
            {
                Heal();
                timeSinceLastHeal = 0f;
            }
        }
    }

    public void FullHeal()
    {
        hp = maxHP;
        if (vignette)
        {
            vignette.intensity.value = initialVignetteIntensity; //set the intensity to initial value
        }
    }

    public void Heal()
    {
        if (hp < maxHP) //if health is not full
        {      
                hp++; //partial healing
                vignette.intensity.value -= increaseIntensityBy; //deccrease the vignette intensity
        }
    }

    public void TakeDamage()
    {
        if (hp > 0) //if player has hp left
        {
            if (!takingDamage) //if not currently taking damage
            {
                takingDamage = true;
                hp--; //take damage
                vignette.intensity.value += increaseIntensityBy; //increase the vignette intensity
                Invoke("StopTakingDamage", damageDuration); //after damageDuration seconds, stop taking damage
            }
        }
        else //player is dead
        {
            GameManager.instance.GameOver(); //signal the game manager that player is dead
            vignette.intensity.value = initialVignetteIntensity; //set the intensity to initial value
        }
    }

    private void StopTakingDamage()
    {
        takingDamage = false;
    }
}
