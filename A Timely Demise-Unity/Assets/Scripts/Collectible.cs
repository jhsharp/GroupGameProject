/**
 * Created By: Ava Fritts
 * Creation Date: April 24 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 24 2022
 * 
 * Description: The base script for all the Collectibles in the game
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    public string collectibleType; //is it a key, a heart, or something else?
    public string playerTag; //the tag used by the player

    public void OnCollisionEnter(Collision collision)
    {
        string colGo = collision.gameObject.tag; //getting the tag from the collision
        if (colGo.Equals(playerTag))  //|| colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            Debug.Log(collectibleType + " found by " + collision.gameObject.tag); //who pressed the button?
            //add to total
        }
        else
        {
            Debug.Log("Not a player object: " + colGo);
        }
    } //end OnCollisionEnter

}
