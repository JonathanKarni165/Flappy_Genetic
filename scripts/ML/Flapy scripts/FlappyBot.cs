using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a movement script for a bird controlled by a simple algorithm
/// </summary>
public class FlappyBot : FlappyInputController
{
    //the next pipe object
    public Transform nextPipe;

    //delta y offset from the pipe's hole from which the bird should jump 
    public float offset;

    //the pipes layer
    public LayerMask pipeLayer;

    //set a delay between jumps
    //the bird should not jump too many times in a row it seems unreal
    private float maxDelay = 0.6f, delay;

    /// <summary>
    /// check whether to jump or not, uses next pipe propeties
    /// </summary>
    /// <returns> true if the bird should jump </returns>
    public virtual bool IsJump()
    {
        if (nextPipe != null)
        {
            if (transform.position.y < nextPipe.GetComponent<PipeSetup>().hole.position.y - offset)
                return true;
        }
        else if (transform.position.y < -3 && nextPipe == null)
            return true;

        return false;
    }

    ///<summary>
    /// update nextPipe variable by shooting a ray and discovering 
    /// each time a new pipe in front of the bird 
    ///</summary>
    public void CheckForPipe()
    {
        //the length of the ray
        //the length should be exectly the space between pipes 
        //in order to discover new pipe each passage
        int length = 14;

        //shoot a ray forward
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, length, pipeLayer);

        //show the ray on screen
        Debug.DrawRay(transform.position, transform.right * length, Color.green, 0.01f);

        //if the ray found a pipe object
        if (hit1)
        {
            if (hit1.transform.gameObject.tag == "pipeFlag")
                nextPipe = hit1.transform.parent;
            else
                nextPipe = hit1.transform;
        }
    }

    private void Update()
    {
        //update next pipe
        CheckForPipe();

        //check if should jump and allowed with delay
        if (IsJump() && delay < 0)
        {
            Jump();

            //restore delay its initial value
            delay = maxDelay;
        }

        delay -= Time.deltaTime;
    }
    
}
