using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// animation script for background
/// </summary>
public class BackgroundAnimation : MonoBehaviour
{
    public float speed;
    public float waitToTerminate;
    public Transform spawnPoint;
    public Transform trigger;

    private bool alreadySpawned = false;

    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (transform.position.x < trigger.position.x && !alreadySpawned)
        {
            Destroy(gameObject, waitToTerminate);
            Instantiate(this, spawnPoint.position, spawnPoint.rotation);
            alreadySpawned = true;
        }
    }
}
