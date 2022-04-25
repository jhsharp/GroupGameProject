/**** 
 * Created by: Ruoyu Zhang
 * Date Created: April 24, 2022
 * 
 * Last Edited by: April 24, 2022
 * Last Edited: April 24, 2022
 * 
 * Description: Updates canvas in game referecing game manager
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvasingame : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    [Header("Canvas SETTINGS")]
    public Text levelTextbox; //textbox for level count
    public Text livesTextbox; //textbox for the lives
    public Text keysTextbox; //textbox for the keys

    //GM Data
    private int level;
    private int totalLevels;
    private int lives;
    private int keys;

    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        //reference to levle info
        level = gm.gameLevelsCount;
        totalLevels = gm.gameLevels.Length;

        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        SetHUD();
    }//end Update()

    void GetGameStats()
    {
        lives = gm.Lives;
        keys = gm.Key;
    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level: " + level + "/" + totalLevels; }
        if (livesTextbox) { livesTextbox.text = "Lives: " + lives; }
        if (keysTextbox) { keysTextbox.text = "Keys: " + keys; }

    }//end SetHUD()
}
