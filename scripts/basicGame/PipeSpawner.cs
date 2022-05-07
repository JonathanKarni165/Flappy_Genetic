using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public bool run;
    public float speed;
    public float increaseSpeedRate, increaseSpeedDelay;
    private float speedTimer, delayTimer;
    public GameObject pipe;
    private float maxSwitchDelay = 1, switchDelay = 1;

    public float delay;

    void Start()
    {
        speedTimer = increaseSpeedDelay;
        //run = true;
        delayTimer = delay;
    }

    private void Update()
    {
        delayTimer -= Time.deltaTime;
        if (delayTimer < 0)
        {
            delayTimer = delay;
            SpawnPipe();
        }

        speedTimer -= Time.deltaTime;
        if (speedTimer < 0)
        {
            IncreaseSpeed(increaseSpeedRate);
            speedTimer = increaseSpeedDelay;
        }

        switchDelay -= Time.deltaTime;
    }

    public void IncreaseSpeed(float amount)
    {
        if (switchDelay > 0)
            return;

        switchDelay = maxSwitchDelay;
        delay = (speed * delay) / (speed + amount);
        speed += amount;

        //update already instantitaed pipes
        PipeSetup[] pipes = FindObjectsOfType<PipeSetup>();
        foreach (PipeSetup pipe in pipes)
        {
            //this class spawned this pipe
            if (pipe.spawner == this)
                pipe.speed = speed;
        }
    }

    public void SpawnPipe()
    {
        pipe.GetComponent<PipeSetup>().speed = speed;
        pipe.GetComponent<PipeSetup>().spawner = this;
        if (run)
            Instantiate(pipe, transform.position, transform.rotation);
    }
}
