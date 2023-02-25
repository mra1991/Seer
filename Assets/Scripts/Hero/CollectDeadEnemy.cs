using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectDeadEnemy : MonoBehaviour
{
    [Tooltip("Attach hint UI element")]
    [SerializeField] private GameObject txtHint;

    private bool canCollect = false;
    private GameObject deadEnemy;

    // Start is called before the first frame update
    void Start()
    {
        txtHint.SetActive(false);  //hide hint
        canCollect = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) //if standing on top of a dead enemy
        {
            deadEnemy = other.gameObject;
            canCollect = true;
            txtHint.SetActive(true);  //show hint
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) //if moving away from a dead enemy
        {
            canCollect = false;
            txtHint.SetActive(false);  //hide hint
        }
    }

    //to be called by the input system
    public void OnCollect(InputAction.CallbackContext context)
    {
        if(context.performed && canCollect) //if the button is pressed and there is a dead enemy to collect
        {
            canCollect = false;
            Destroy(deadEnemy); //destroy the enemy gameobject
            GameManager.instance.AddPoints(); //signal game manager increase the number of collected enemies
            txtHint.SetActive(false);  //hide hint
        }
    }

    
}
