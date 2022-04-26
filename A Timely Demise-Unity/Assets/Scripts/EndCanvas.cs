/**** 
 * Created by: Ruoyu Zhang
 * Date Created: April 24, 2022
 * 
 * Last Edited by: April 24, 2022
 * Last Edited: April 24, 2022
 * 
 * Description: Updates end canvas referencing the game manager
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    GameManager gm; //reference to game manager

    public GameManager GameManager
    {
        get => default;
        set
        {
        }
    }

    private void Start()
    {
        gm = GameManager.GM; //find the game manager

    }

    public void GameRestart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager

    }

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager

    }
}
