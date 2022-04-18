/**
 * Created By: Ava Fritts
 * Creation Date: April 18 2022
 * 
 * Updated by: Ava Fritts
 * Updated Date: April 18 2022
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockDoor()
    {
        Debug.Log("Door unlocked");
    }

    public void LockDoor()
    {
        Debug.Log("Door locked");
    }
}
