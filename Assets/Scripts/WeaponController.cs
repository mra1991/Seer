using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Tooltip("Attach the main camera here.")]
    [SerializeField] private Transform cam;
    private Animator anim;
    private AudioSource audio;

    //variables for firing
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer fireLine;
    const float fireLineDuration = 0.3f;

    //variables for fire2 (throwing projectile)
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileThrowForce = 30f;
    [SerializeField] private AudioClip powerupSoundClip;
    [SerializeField] private float maxPowerupDuration = 1f;
    private float powerupDuration = 0f;
    private bool poweringUp = false;

    // Start is called before the first frame update
    void Start()
    {
        //Cache Animator component 
        anim = GetComponent<Animator>();
        //Cache AudioSource component
        audio = GetComponent<AudioSource>();
        StopDrawLineOfFire();
    }

    // Update is called once per frame
    void Update()
    {
        if (poweringUp && powerupDuration < maxPowerupDuration)
        {
            powerupDuration += Time.deltaTime;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {       
        if (!GameManager.instance.Pause && context.performed)
        {
            anim.SetTrigger("Fire");  //play weapon fire animation
            audio.PlayOneShot(audio.clip); //play weapon fire sound
            RaycastHit hit;
            if(Physics.Raycast(cam.position,cam.forward,out hit, fireRange)) //if something's hit
            {
                //mark the path of the ray with a red line for debugging purposes
                Debug.DrawRay(cam.position, cam.forward * hit.distance, Color.red);
                DrawLineOfFire(hit.point, true); //adjust and display the line renderer
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    //signal the enemy to take damage
                    hit.collider.gameObject.GetComponent<EnemyBehaviour>().TakeDamage();
                }
            }
            else //raycast didn't hit anything
            {
                Debug.DrawRay(cam.position, cam.forward * fireRange, Color.yellow);
                DrawLineOfFire(Vector3.zero, false); //adjust and display the line renderer
            }
        }
    }

    public void OnFire2(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.Pause && context.started)
        {
            poweringUp = true;
            powerupDuration = 0f;
            audio.PlayOneShot(powerupSoundClip);
        }
        if (!GameManager.instance.Pause && context.canceled)
        {
            anim.SetTrigger("Fire");  //play weapon fire animation
            poweringUp = false;
            GameObject newProjectile = Instantiate(projectile, firePoint.position, cam.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(cam.forward * projectileThrowForce * powerupDuration, ForceMode.Impulse);
            powerupDuration = 0f;
        }
    }

    private void DrawLineOfFire(Vector3 hitPoint, bool didHit )
    {
        fireLine.enabled = true;
        Vector3[] linePoints = new Vector3[2];
        linePoints[0] = firePoint.position; //begin from firing point of the weapon
        if (didHit)
        {
            linePoints[1] = hitPoint;
        }
        else
        {
            linePoints[1] = cam.position + cam.forward * fireRange;
        }
        fireLine.positionCount = 2;
        fireLine.SetPositions(linePoints);
        Invoke("StopDrawLineOfFire", fireLineDuration);
    }

    private void StopDrawLineOfFire()
    {
        fireLine.enabled = false;
    }
}
