/****
 * Created By: Jacob Sharp
 * Date Created: April 18, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: April 20, 2022
 * 
 * Description: Record the positions of an object and play it back later
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecorder : MonoBehaviour
{
    // booleans to determine behavior
    public bool isRecording = true;
    public bool isReplaying = false;

    // frames between each recorded position
    public int recordSpacing = 0;
    private int recordTimer = 0;

    // current time in replay process
    private int replayIndex;

    // list to store inputs and variables for input/position
    private List<InputSet> inputList = new List<InputSet>();
    private Vector3 initialPosition;
    private bool hasJumped = false;

    // reference to game component
    private Rigidbody rb;
    private PlayerController player;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
        if (isRecording) startRecord();
    }

    private void Update()
    {
        // manually start replaying
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startReplay();
            Debug.Log("Starting Replay");
        }
    }

    void FixedUpdate()
    {
        // manage recording behavior
        if (isRecording)
        {
            if (recordTimer <= 0)
            {
                recordInput();
                recordTimer = recordSpacing;
            }
            else recordTimer--;
        }

        // manage replaying behavior
        else if (isReplaying)
        {
            replayPosition(); // NEED TO ADD INTERPOLATION BETWEEN POINTS
        }
    }

    private void recordInput()
    {
        InputSet nextInput = new InputSet(player.moveInput, player.hasJumped);
        if (player.hasJumped) player.hasJumped = false;
        inputList.Add(nextInput);
    }

    private void replayPosition()
    {
        // increment the replay index if a new recorded input is available
        if (recordTimer == 0)
        {
            recordTimer = recordSpacing;
            replayIndex++;
        }
        else recordTimer--; // otherwise keep counting towards the next recorded input time

        // replay positions from the list
        if (replayIndex < inputList.Count)
        {
            InputSet input = inputList[replayIndex];
            player.moveInput = input.move;
            player.jumpInput = input.jump;
        }
        // if recording is ended, stop replaying
        else
        {
            isReplaying = false;
            player.moveInput = 0;
            player.jumpInput = false;
        }

    }

    public void startRecord()
    {
        isRecording = true;
        isReplaying = false;
        player.controlsEnabled = true;
        inputList.Clear();
        initialPosition = transform.position;

    }

    public void startReplay()
    {
        isRecording = false;
        isReplaying = true;
        player.controlsEnabled = false;
        replayIndex = -1;
        recordTimer = 0;
        transform.position = initialPosition;
    }

    public struct InputSet
    {
        // a struct used to contain all of the input from player
        public float move;
        public bool jump;

        public InputSet(float moveInput, bool jumpInput)
        {
            move = moveInput;
            jump = jumpInput;
        }
    }
}
