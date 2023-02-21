using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("Maximum Health Points.")] 
    [SerializeField] private int maxHP = 10;
    [Tooltip("How long 'till dead enemy disappears.")]
    [SerializeField] private float deadBodyLifeTime = 30f;
    private int hp;
    private bool isDead = false;
    private Animator anim;
    private bool isAttacking = false;
    private GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAttacking)
        {
            hero.GetComponent<SeerDamage>().TakeDamage(); //signal the player to take damage

        }
        anim.SetFloat("Velocity", gameObject.GetComponent<Rigidbody>().velocity.magnitude); //give animator objects velocity
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hero = collision.gameObject;
            isAttacking = true;
            anim.SetBool("Attacking", true); //change the animation state to attacking
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            anim.SetBool("Attacking", false); //go back to blend tree
        }
    }

    public void TakeDamage()
    {
        if (hp > 0)
        {
            hp--;
        }
        else
        {
            //enemy is dead
            isDead = true;
            GameManager.instance.EnemyDown(); //signal the game manager that an enemy has died
            anim.SetTrigger("Death"); //play death animation
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true; //turn the collider a trigger, so it won't restrict player's movement
            Invoke("DecomposeBody", deadBodyLifeTime); //after a while destroy yourself
        }

    }

    private void DecomposeBody()
    {
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
