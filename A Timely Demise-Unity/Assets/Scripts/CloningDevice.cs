/****
 * Created By: Jacob Sharp
 * Date Created: April 20, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: April 24, 2022
 * 
 * Description: Create a clone of the player when activated
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningDevice : MonoBehaviour
{
    [Header("SET IN INSPECTOR:")]
    [Tooltip("Should be set to true for the starting point for the level (false for all others)")]
    public bool activated = false;// indicates whether the cloning device has already been activated

    [Header("ONLY FOR PREFAB SETTINGS:")]
    [Tooltip("Make sure this player is set to inactive in the editor")]
    public GameObject player; // reference to the player associated with this device
    public Material activatedMat;

    private Renderer renderer;

    void Start()
    {
        // Get a reference to the renderer
        renderer = GetComponent<Renderer>();

        // Activate the player if this is the starting point of the level
        if (activated)
        {
            player.SetActive(true);
            renderer.material = activatedMat; // set new material
        }
    }

    private void OnEnable()
    {
        // reset the body list when a scene first loads
        TimeRecorder.resetBodyList();
    }

    private void OnTriggerEnter(Collider col)
    {
        // If unactivated, activate when a player enters the trigger
        if (!activated)
        {
            Debug.Log("Activating New Cloning Device");
            GameObject other = col.gameObject;
            if (other.GetComponent<PlayerController>() != null) activate();
        }
    }

    private void activate()
    {
        // Activate the new player body and restart all clones
        activated = true;
        renderer.material = activatedMat;
        TimeRecorder.globalReplay(); // needs to be done before the new player activates to ensure the new player records instead of replaying
        player.SetActive(true);
    }
}
