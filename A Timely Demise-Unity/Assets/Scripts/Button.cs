/**
 * Created By: Ava Fritts
 * Creation Date: April 18 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 18 2022
 * 
 * Description: The base script for all the buttons in the game
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    /**Variables**/
    [Header("Button settings")]
    public bool isPressed; //is something pressing the button?
    public string playerTag; //the tag used by the player
    public string playerMimicTag; //the tag used by the player copies.

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;  
    }

    public void OnCollisionEnter(Collision collision)
    {
        string colGo = collision.gameObject.tag; //getting the tag from the collision
        if(colGo.Equals(playerTag) || colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            isPressed = true;
            Debug.Log("Button pressed by " + collision.gameObject.tag);
        }
        else
        {
            Debug.Log("Not a player object");
        }
    } //end OnCollisionEnter

    public void OnCollisionExit(Collision collision) 
    {
        string colGo = collision.gameObject.tag;
        if (colGo.Equals(playerTag) || colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            isPressed = false;
            Debug.Log("Button left by " + collision.gameObject.tag);
        }
        else
        {
            Debug.Log("Not a player object");
        }
    } //end OnCollisionExit
}
