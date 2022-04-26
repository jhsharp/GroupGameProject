/**
 * Created By: Ava Fritts
 * Creation Date: April 18 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 20 2022
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
    public LayerMask playerCharacters; //the layer used by the characters
    public string playerTag; //the tag used by the player
    //public string playerMimicTag; //the tag used by the player copies.
    public GameObject connectedDoor; //the door associated with this button.
    private Door unlockMech;

    public Door Door
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        unlockMech = connectedDoor.GetComponent<Door>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        string colGo = collision.gameObject.tag; //getting the tag from the collision
        if (colGo.Equals(playerTag))  //|| colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            isPressed = true; //tell the game the button has been pressed
            Debug.Log("Button pressed by " + collision.gameObject.tag); //who pressed the button?
            unlockMech.UnlockDoor(); //unlock the connected door
        }
        else
        {
            Debug.Log("Not a player object: "+ colGo);
        }
    } //end OnCollisionEnter

    public void OnCollisionExit(Collision collision)
    {
        string colGo = collision.gameObject.tag; //getting the tag from the collision
        if (colGo.Equals(playerTag)) //|| colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            isPressed = false; //tell the game the button is not pressed
            Debug.Log("Button left by " + collision.gameObject.tag); //who left the button?
            unlockMech.LockDoor(); //lock the door
        }
        else
        {
            Debug.Log("Not a player object");
        }
    } //end OnCollisionExit
}
