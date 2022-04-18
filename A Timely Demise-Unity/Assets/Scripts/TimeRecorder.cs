/****
 * Created By: Jacob Sharp
 * Date Created: April 18, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: April 18, 2022
 * 
 * Description: Record the positions of an object and play it back later
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecorder : MonoBehaviour
{
    // booleans to determine behavior
    public bool record = true;
    public bool playback = false;

    // frames between each recorded position
    public int recordSpacing = 5;
    private int recordTimer = 0;

    // list to store positions in
    public List<Vector3> positionList = new List<Vector3>();

    void Start()
    {
        Invoke("RecordPosition", 0);
    }

    private void RecordPosition()
    {
        positionList.Add(gameObject.transform.position);
    }
}
