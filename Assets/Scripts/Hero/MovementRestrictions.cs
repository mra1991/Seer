using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementRestrictions : MonoBehaviour
{
    private Vector3 prevPos=Vector3.zero; //previous postion. used to reset the player's position, if they've gone where they're not supposed to

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.instance.PauseOrPlay();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        //raycast from player's position, direction dow, output hit
        if(Physics.Raycast(transform.position,Vector3.down,out hit, 1000))
        {
            //if the ray hits water
            if (hit.collider.CompareTag("Water"))
            {
                //reset players position, but only in x and z (we don't want to freeze the player mid-air)
                transform.position = new Vector3(prevPos.x, transform.position.y, prevPos.z);
            }
            else
            {
                prevPos = transform.position; //update previous position
                if (hit.collider.CompareTag("Fire")) //if the ray hits fire
                {
                    gameObject.GetComponent<SeerDamage>().TakeDamage(); //signal the script for taking damage
                }
            }
        }
    }
}
