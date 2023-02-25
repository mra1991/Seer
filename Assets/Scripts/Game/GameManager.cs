using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //declaration of singleton instance
    public static GameManager instance = null;

    //keep track of players position and orientation in the world
    [Tooltip("Attach your hero here")]
    [SerializeField] public Transform hero;
    [Tooltip("Attach your hero here")]
    [SerializeField] private SeerDamage seerDamage;

    //variables for pause menu and game over
    private bool pause = false;
    private float oldTime = 0f;
    private bool gameOver = false;
    [Tooltip("Attach eventsystem with SelectMenu script on it")]
    [SerializeField] private SelectMenu selectMenu;
    [Tooltip("Attach UI element for GAME OVER text")]
    [SerializeField] GameObject txtGameOver; //text mesh pro for GAME OVER

    //variables for score
    [SerializeField] TMP_Text tmpScore; //text mesh pro for score
    private int score = 0;

    public bool Pause { get => pause; }

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
        LoadGame();

        selectMenu.PanelToggle(-1); //tell selectMenu to hide all panels
        //lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        //don't show cursor
        Cursor.visible = false;

        DisplayScore();
        txtGameOver.SetActive(false); //hide game over UI element

        seerDamage.FullHeal();
    }

    //Reloads the scene
    public void InitGame()
    {
        //make sure to unpause the game before reloading the scene
        if (pause) 
        {
            gameOver = false;
            PauseOrPlay();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DisplayScore()
    {
        tmpScore.text = score.ToString("D4");
    }

    //function to be called by the scrip on enemy
    public void AddPoints()
    {
        score++; //add score
        DisplayScore(); //refresh the score
    }

    //to be called when the character dies (by script SeerDamage)
    public void GameOver()
    {
        txtGameOver.SetActive(true);
        if(!pause)
            PauseOrPlay();
        gameOver = true;
    }

    public void PauseOrPlay()
    {
        if (!gameOver) //if player's not dead
        {
            pause = !pause;
            //hero.GetComponent<RigidbodyFirstPersonController>().enabled = !pause;
            selectMenu.PanelToggle(pause ? 0 : -1); //show first panel/hide all panels

            //swap current and old time scales
            float temp = oldTime;
            oldTime = Time.timeScale;
            Time.timeScale = temp;

            Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = pause ? true : false;
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
        InitGame();
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

    }
}
