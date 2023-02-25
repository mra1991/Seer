using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //declaration of singleton instance
    public static EnemyManager instance = null;

    //variables for generating enemies
    [SerializeField] private int maxNumberOfEnemies = 10;
    private int currNumOfEnemies = 0;
    [SerializeField] private float generateEnemyEvery = 10f;
    private float timeSinceLastEnemy = 0f;
    [SerializeField] private Transform appearingSpot;
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        //singleton design
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //refreshes the text for score
   

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastEnemy < generateEnemyEvery)
        {
            timeSinceLastEnemy += Time.deltaTime;
        }
        else if (currNumOfEnemies < maxNumberOfEnemies) //enough time has passed and not enough enemies there are
        {
            Instantiate(enemy, appearingSpot.position, Quaternion.identity); //instantiate new enemy
            currNumOfEnemies++; //increase the current number of enemies
            timeSinceLastEnemy = 0f;
        }
    }

    public void EnemyDown()
    {
        currNumOfEnemies--;
    }
}
