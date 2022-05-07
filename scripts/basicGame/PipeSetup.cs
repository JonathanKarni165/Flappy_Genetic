using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// pipe (the main obstacle) setup and movement script
/// holds public references to it's hole importent positions
/// </summary>
public class PipeSetup : MonoBehaviour
{
    //the script that spawned this gameobject
    public PipeSpawner spawner;
  
    public float speed;

    //the random range of the hole places
    public float lowR, highR;
    //the pipe's bottom and top colliders
    public Collider2D col1, col2;

    //several importent positions of the hole
    //used by other scripts
    public Transform hole, trigger, upperEdge, lowerEdge;

    private void Awake()
    {
        Setup();
    }

    /// <summary>
    /// chooses a random place to put the pipe's hole 
    /// moves the collider accordingly
    /// </summary>
    private void Setup()
    {
        //moving the hole object
        hole.localPosition = new Vector2(0 ,Random.Range(lowR, highR));
        trigger.localPosition = hole.localPosition;

        //moving the colliders 
        col1.offset = new Vector2(0, (-0.5f + hole.localPosition.y) - (hole.localScale.y / 2));
        col2.offset = new Vector2(0, (0.5f + hole.localPosition.y) + (hole.localScale.y / 2));
    }

    private void Update()
    {
        //moving the pipe at given speed
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));

        //end of screen -> self destruct
        if (transform.position.x < -25)
            Destroy(gameObject);
    }
}
