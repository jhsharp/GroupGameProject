/**
 * Created By: Ava Fritts
 * Creation Date: April 18 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 20 2022
 * 
 * Description: The base script for all the doors in the game
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isOpen;
    public Vector3 LockedPosition; //the standard door position
    public Vector3 UnlockedPosition; //the open door position


    // Start is called before the first frame update
    void Start()
    {
      //add line to get the current position
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockDoor()
    {
        Debug.Log("Door unlocked");
        gameObject.transform.position = UnlockedPosition; //open the door
    }

    public void LockDoor()
    {
        Debug.Log("Door locked");
        gameObject.transform.position = LockedPosition; //close the door
    }
}
