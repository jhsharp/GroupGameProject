/****
 * Created by: Ruoyu Zhang
 * Data Created: April 23, 2022
 * 
 * Last Edited by: April 23, 2022
 * Last Edited: April 23, 2022
 * 
 * Description: Create the goal of the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // A static field accessible by code anywhere
    static public bool goalMet = false;
    void OnTriggerEnter(Collider other)
    {
        // When the trigger is hit by something
        // Check to see if it's a Man
        if (other.gameObject.tag == "Player")
        {
            // If so, set goalMet to true
            Goal.goalMet = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
