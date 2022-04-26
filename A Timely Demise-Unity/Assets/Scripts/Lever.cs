/**
 * Created By: Ava Fritts
 * Creation Date: April 20 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 20 2022
 * 
 * Description: The base script for all the Levers in the game
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    /**Variables**/
    [Header("Button settings")]
    public bool isPressed; //is something pressing the button?
    private SpriteRenderer spriteRenderer;
    public string playerTag; //the tag used by the player
    public string playerMimicTag; //the tag used by the player copies.
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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        string colGo = collision.gameObject.tag; //getting the tag from the collision
        if (colGo.Equals(playerTag) || colGo.Equals(playerMimicTag)) //if the tag matches the tag of the player characters
        {
            Debug.Log("Button pressed by " + collision.gameObject.tag); //who pressed the button?
            SwapState();
        }
        else
        {
            Debug.Log("Not a player object");
        }
     } //end OnCollisionEnter

    public void SwapState()
    {
        if (isPressed == true)
        {
            isPressed = false;
            unlockMech.LockDoor();
            spriteRenderer.flipY = true;
        }
        else
        {
            isPressed = true; //tell the game the button has been pressed
            unlockMech.UnlockDoor(); //unlock the connected door
            spriteRenderer.flipY = false;
        }
    }
      

   
}
