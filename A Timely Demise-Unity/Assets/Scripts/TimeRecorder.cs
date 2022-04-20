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
    private int replayTime;

    // list to store positions in
    public List<Vector3> positionList = new List<Vector3>();

    // reference to game component
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                recordPosition();
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

    private void recordPosition()
    {
        positionList.Add(transform.position);
    }

    private void replayPosition()
    {
        // replay positions from the list
        if (replayTime < positionList.Count)
        {
            transform.position = positionList[replayTime];
            replayTime++;
        }

        // if recording is ended, stop replaying
        else isReplaying = false;
    }

    public void startReplay()
    {
        isRecording = false;
        isReplaying = true;
        replayTime = 0;
        recordTimer = 0;
        rb.isKinematic = true;
    }
}
