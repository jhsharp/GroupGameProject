/**** 
 * Created by: Ruoyu Zhang
 * Date Created: April 24, 2022
 * 
 * Last Edited by: April 24,2022
 * Last Edited: April 24, 2022
 * 
 * Description: GameManager
****/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { Title, Playing, BeatLevel, LostLevel, GameOver, Idle };//enum of game states (work like it's own class)

public class GameManager : MonoBehaviour
{
    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(gm);
    }//end CheckGameManagerIsInScene()
    #endregion

    //Game State Varaiables
    [HideInInspector] public GameState gameState = GameState.Title; //first game state

    [Header("GAME SETTINGS")]

    [Space(10)]

    //static vairables can not be updated in the inspector, however private serialized fileds can be
    [SerializeField] //Access to private variables in editor
    private int numberOfLives; //set number of lives in the inspector
    [Tooltip("Does the level get reset when a life is lost")]
    public bool resetLostLevel = true; //reset the lost level
    static public int lives; // number of lives for player 
    public int Lives { get { return lives; } set { lives = value; } }//access to static variable lives [get/set methods]

    static public int reset;  //key value
    public int Reset { get { return reset; } set { reset = value; } }//access to static variable key [get/set methods]

    [Space(10)]
    public string defaultEndMessage = "Game Over";//the end screen message, depends on winning outcome
    [HideInInspector] public string endMsg;//the end screen message, depends on winning outcome

    [Header("SCENE SETTINGS")]
    [Tooltip("Name of the start scene")]
    public string startScene;

    [Tooltip("Name of the game over scene")]
    public string gameOverScene;

    [Tooltip("Count and name of each Game Level (scene)")]
    public string[] gameLevels; //names of levels
    [HideInInspector]
    public int gameLevelsCount; //what level we are on
    private int loadLevel; //what level from the array to load

    public static string currentSceneName; //the current scene name;

    [SerializeField] //Access to private variables in editor
    [Tooltip("Check to test player lost the level")]
    private bool levelLost = false;//we have lost the level (ie. player died)

    //test next level
    [SerializeField] //Access to private variables in editor
    public bool nextLevel = false; //test for next level

    //Win/Loose conditon
    [SerializeField] //Access to private variables in editor
    private bool playerWon = false;


    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        //runs the method to check for the GameManager
        CheckGameManagerIsInScene();

        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    }//end Awake()


    // Update is called once per frame
    private void Update()
    {
        //if ESC is pressed , exit game
        if (Input.GetKey("escape")) { ExitGame(); }

        //check for game state changes
        CheckGameState();

        //Outpot game state
        Debug.Log("Game State " + gameState);

    }//end Update


    //SET GAME STATES
    public void SetGameState(GameState state)
    {
        this.gameState = state;
    }//end SetGameState()


    //CHECK FOR GAME STATE CHANGES
    private void CheckGameState()
    {
        switch (gameState)
        {
            case GameState.Title:
                //do nothing
                break;

            case GameState.Playing:
                Updatereset();
                if (PlayerController.dead) { PlayerController.dead = false;  LostLife(); }
                if (Goal.goalMet) { Goal.goalMet = false; NextLevel();}
                break;

            case GameState.BeatLevel:
                Debug.Log("beat level");
                NextLevel(); //check for the next level
                break;

            case GameState.LostLevel:
                GameOver(); //move to game over
                break;

            case GameState.GameOver:
                //do nothing
                break;

            case GameState.Idle:
                //do nothing
                break;
        }//end switch(gameStates)
    }//end CheckGameState()


    //LOAD THE GAME FOR THE FIRST TIME OR RESTART
    public void StartGame()
    {
        //get first game level
        gameLevelsCount = 1; //set the count for the game levels
        loadLevel = gameLevelsCount - 1; //the level from the array

        //load first game level
        PlayerPrefs.SetInt("Current Level", gameLevelsCount); // update save data
        SceneManager.LoadScene(gameLevels[loadLevel]);

        SetDefaultGameStats(); // the game stats defaults 

    }//end StartGame()

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("Current Level")) StartGame(); // if there is no save data, start a new game
        else // otherwise, load the stored level
        {
            SetDefaultGameStats(); // the game stats defaults 
            gameLevelsCount = PlayerPrefs.GetInt("Current Level") - 1; // set level
            NextLevel();
        }
    }


    public void SetDefaultGameStats()
    {
        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;


        //SET ALL GAME LEVEL VARIABLES FOR START OF GAME
        lives = numberOfLives; //set the number of lives


        endMsg = defaultEndMessage; //set the end message default

        playerWon = false; //set player winning condition to false

        SetGameState(GameState.Playing);//set the game state to playing

    }//end SetDefaultGameStats()


    //EXIT THE GAME
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited Game");
    }//end ExitGame()


    //GO TO THE GAME OVER SCENE
    public void GameOver()
    {
        SetGameState(GameState.GameOver);//set the game state to Game Over

        SceneManager.LoadScene(gameOverScene); //load the game over scene

    }//end GameOver()


    //GO TO THE NEXT LEVEL
    void NextLevel()
    {

        //as long as our level count is not more than the amount of levels
        if (gameLevelsCount < gameLevels.Length)
        {
            gameLevelsCount++; //add to level count for next level
            loadLevel = gameLevelsCount - 1; //find the next level in the array
            PlayerPrefs.SetInt("Current Level", gameLevelsCount); // update save data
            SceneManager.LoadScene(gameLevels[loadLevel]); //load next level
            SetGameState(GameState.Playing);//set the game state to playing


        }
        else
        { //if we have run out of levels go to game over
            GameOver();
        } //end if (gameLevelsCount <=  gameLevels.Length)

    }//end NextLevel()

    public void Updatereset(int point = 0)
    { //This method manages the score on update. 

        if (PlayerController.dead)
        {
            point++;
        }
        reset += point;
        PlayerPrefs.SetInt("Reset Counter:", reset); //set the playerPref for the high score

    }//end CheckScore()

    //PLAYER LOST A LIFE
    public void LostLife()
    {
        if (lives == 1) //if there is one life left and it is lost
        {
            GameOver(); //game is over

        }
        else
        {
            lives--; //subtract from lives reset level lost 

            //if this level resets when life is lost
            numberOfLives = lives; //set lives left for level reset

        } // end elseif
    }//end LostLife()

    // Restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
