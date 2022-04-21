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

    // static list to store all clone bodies
    private static List<TimeRecorder> bodyList = new List<TimeRecorder>();

    // reference to game component
    private Rigidbody rb;
    private PlayerController player;


    private void Start()
    {
        // get references to components
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();

        // add self to global list of bodies
        bodyList.Add(this);

        // start recording the player
        if (isRecording) startRecord();
    }

    private void Update()
    {
        // manually start replaying (for testing purposes)
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            startReplay();
            Debug.Log("Starting Replay");
        }*/
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
            replayPosition();
        }
    }

    private void recordInput()
    {
        // record the move and jump input from the player
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

        // replay inputs from the list
        if (replayIndex < inputList.Count)
        {
            InputSet input = inputList[replayIndex];
            player.moveInput = input.move;
            if (recordTimer == recordSpacing) player.jumpInput = input.jump; // only jump on the first jump frame
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
        // reset variables to start recording
        isRecording = true;
        isReplaying = false;
        player.controlsEnabled = true;
        inputList.Clear();

        // mark initial position of player
        initialPosition = transform.position;

    }

    public void startReplay()
    {
        // reset variables to start replaying
        isRecording = false;
        isReplaying = true;
        player.controlsEnabled = false;
        replayIndex = -1;
        recordTimer = 0;

        // move clone to original position
        transform.position = initialPosition;
    }

    public static void globalReplay()
    {
        // make all existing clones restart the replay process
        foreach (TimeRecorder recorder in bodyList)
        {
            recorder.startReplay();
        }
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
