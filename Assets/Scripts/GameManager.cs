using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //declaration of singleton instance
    public static GameManager instance = null;

    //keep track of players position and orientation in the world
    [Tooltip("Attach your hero here")]
    [SerializeField] public Transform hero;
    [Tooltip("Attach your hero here")]
    [SerializeField] private SeerDamage seerDamage;

    //variables for generating enemies
    [SerializeField] private int maxNumberOfEnemies = 10;
    private int currNumOfEnemies = 0;
    [SerializeField] private float generateEnemyEvery = 10f;
    private float timeSinceLastEnemy = 0f;
    [SerializeField] private Transform appearingSpot;
    [SerializeField] private GameObject enemy;

    //variables for score
    [SerializeField] TMP_Text tmpScore; //text mesh pro for score
    private int score = 0;

    //variables for pause menu and game over
    private bool pause = false;
    private bool gameOver = false;
    [Tooltip("Attach eventsystem with SelectMenu script on it")]
    [SerializeField] private SelectMenu selectMenu;
    [Tooltip("Attach UI element for GAME OVER text")]
    [SerializeField] GameObject txtGameOver; //text mesh pro for GAME OVER

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
        InitGame();
    }

    private void InitGame()
    {
        Time.timeScale = 1f; //make sure time is not freezed (pause)
        LoadGame(); //fetch score and player's position from player prefs and set them      
        HideMenu();
        DisplayScore();
        txtGameOver.SetActive(false); //hide game over UI element
        HideMenu();
        seerDamage.FullHeal();
    }


    //refreshes the text for score
    void DisplayScore()
    {
        tmpScore.text = score.ToString("D4");
    }

    void ShowMenu()
    {
        selectMenu.PanelToggle(0); //tell selectMenu to show first panel, which is the pause menu
    }

    void HideMenu()
    {
        selectMenu.PanelToggle(-1); //tell selectMenu to hide all panels
    }


    //function to be called by the scrip on enemy
    public void AddPoints()
    {
        score ++; //add score
        DisplayScore(); //refresh the score
    }


    //to be called when the character dies (by script SeerDamage)
    public void Death()
    {
        GameOver();
    }

    void GameOver()
    {
        gameOver = true;
        txtGameOver.SetActive(true);
        ShowMenu();
    }

    public void PauseOrPlay()
    {
        if (!gameOver) //if player's not dead
        {
            if (!pause) //if not pause, pause the game
            {
                pause = true;
                //Time.timeScale = 0f;
                ShowMenu();
            }
            else //game is paused, unpause it
            {
                pause = false;
                Time.timeScale = 1f;
                HideMenu();
            }
        }
        else if (!pause) //the player's dead and the game is not paused, so pause it
        {
            pause = true;
            //Time.timeScale = 0f;
            ShowMenu();
        }
    }

 
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetFloat("HeroPosX", hero.position.x);
        PlayerPrefs.SetFloat("HeroPosY", hero.position.y);
        PlayerPrefs.SetFloat("HeroPosZ", hero.position.z);
        PlayerPrefs.SetFloat("HeroRotX", hero.eulerAngles.x);
        PlayerPrefs.SetFloat("HeroRotY", hero.eulerAngles.y);
        PlayerPrefs.SetFloat("HeroRotZ", hero.eulerAngles.z);
        PlayerPrefs.Save();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("HeroPosX", 300f);
        PlayerPrefs.SetFloat("HeroPosY", 30.65f);
        PlayerPrefs.SetFloat("HeroPosZ", 181f);
        PlayerPrefs.SetFloat("HeroRotX", 0f);
        PlayerPrefs.SetFloat("HeroRotY", 45f);
        PlayerPrefs.SetFloat("HeroRotZ", 0f);
        InitGame(); //reinitialize the game
    }

    public void LoadGame()
    {

        score = PlayerPrefs.GetInt("Score", 0);
        hero.position = new Vector3(
            PlayerPrefs.GetFloat("HeroPosX", 300f),
            PlayerPrefs.GetFloat("HeroPosY", 30.65f),
            PlayerPrefs.GetFloat("HeroPosZ", 181f)
            );
        hero.eulerAngles = new Vector3(
            PlayerPrefs.GetFloat("HeroRotX", 0f),
            PlayerPrefs.GetFloat("HeroRotY", 45f),
            PlayerPrefs.GetFloat("HeroRotZ", 0f));
    }


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
