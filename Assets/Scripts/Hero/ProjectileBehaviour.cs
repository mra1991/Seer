using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject smoke; //attach the prefab for smoke 
    [SerializeField] private int projectileDamage = 4; //projectile damage to regular damage ratio
    [SerializeField] private float explosionRadius = 10f; //enemies in this range take damage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //if projectile hits an enemy directly
        {
            for (int i = 0; i < projectileDamage; i++) //take damage multiple times since it's a projectile
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(); //signal the enemy to take damage
            }
        }
        //ovelap sphere from explosion point
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Enemy")) //if an enemy in range
            {
                //signal the enemy to take damage
                c.gameObject.GetComponent<EnemyBehaviour>().TakeDamage();
            }
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //leave a trail of smoke before destroying yourself
        Instantiate(smoke, transform.position, transform.rotation);
    }
}
