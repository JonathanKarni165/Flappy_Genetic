using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a class that will preserve certein gameobject over scene loading
/// </summary>
public class DDOL : MonoBehaviour
{
    void Awake()
    {
        //find the specified game objects with tag
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DDOS");

        //delete new spawned game objects that already exist 
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    //reset all training gameobjects
    //called by game manager when pressed esc
    public void DestroyOnNextLoad()
    {
        //reset generations
        TrainingManager.genNum = 0;

        //destroy this game object
        Destroy(gameObject);
    }
}