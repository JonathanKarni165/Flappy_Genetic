using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// movement script for a bird controlled by a neural network
/// </summary>
public class FlappyNeural : FlappyBot
{
    //used for game scene to check if this is the enemy bird
    public bool isEnemyPlayer;

    //the neural network of the bird
    public NeuralNetwork net;

    //used for loading a neural network from a saved txt file
    public TextAsset netTextSource;

    //net input
    float deltaX, velocityY, disHigh, disLow, pipeSpeed;

    //time spent alive during this scene
    float aliveDuration;

    //how many pipes to pass to increase speed
    //used for training purposes
    private int pipesToSwitch;

    private void Start()
    {
        //if this is the enemy bird playing in the game scene 
        //we should load its own neural network
        if (isEnemyPlayer)
            InitNet();

        aliveDuration = 0;
    }

    /// <summary>
    /// initialize a new neural network for this bird using a netset
    /// </summary>
    /// <param name="netSet"> an array that represents how many neurons in each layer</param>
    public void InitNet(int[] netSet)
    {
        net = new NeuralNetwork(netSet);
    }

    /// <summary>
    /// initialize a new neural network for this bird using another net
    /// </summary>
    /// <param name="other"> nother neural network that we want to copy </param>
    public void InitNet(NeuralNetwork other)
    {
        net = other;
    }

    /// <summary>
    /// initialize a new neural network for this bird using another net that is saved on a txt file
    /// </summary>
    public void InitNet()
    {
        net = NetSerialize.Deserialize(netTextSource);
    }

    /// <summary>
    /// check whether to jump by neural network output
    /// </summary>
    /// <returns> true if the bird should jump given the inputs</returns>
    public override bool IsJump()
    {
        //when pipe is not found jump in place 
        if (transform.position.y < -3 && nextPipe == null)
            return true;

        else if(nextPipe != null)
        {
            //the middle of the next hole
            Transform hole = nextPipe.GetComponent<PipeSetup>().hole.transform;

            //the upper edge of the next hole
            Transform upEdge = nextPipe.GetComponent<PipeSetup>().upperEdge.transform;

            //the lower edge of the next hole
            Transform lowEdge = nextPipe.GetComponent<PipeSetup>().lowerEdge.transform;

            //the next pipe current speed
            pipeSpeed = nextPipe.GetComponent<PipeSetup>().speed;

            //distance in horizontal exis from the hole
            deltaX = hole.position.x - transform.position.x;

            //speed in the y axis (falling speed)
            velocityY = rb.velocity.y;

            //distance in the y axis from upper edge of the hole
            disHigh = upEdge.position.y - transform.position.y;

            //distance in the y axis from lower edge of the hole
            disLow = transform.position.y - lowEdge.position.y;

            //the input array that will feed the network
            float[] input = { deltaX, velocityY, disHigh, disLow, pipeSpeed};

            if (input != null)
            {
                //input the network the input array
                net.FeedInput(input);

                //calculate all neurons
                net.FeedForward();
            }

            //return the neural net output
            return net.GetOutput();
        }
        
        return false;
    }

    void Update()
    {
        //play until reached to train target
        if (FindObjectOfType<TrainingManager>() != null)
            if (FindObjectOfType<TrainingManager>().targetScore < score)
                return;

        //add to time spent alive during this generation
        aliveDuration += Time.deltaTime;

        //update next pipe
        CheckForPipe();

        if (IsJump())
            Jump();

        //decerementing from the invinsibility duration
        hitCoolDown -= Time.deltaTime;
    }

    /// <summary>
    /// the death of a bird game object in the training scene
    /// </summary>
    public void TrainedBirdDeath()
    {
        print("TrainedBirdDeath");
        net.fitness = aliveDuration;
        net.score = score;
        gameObject.SetActive(false);

        FindObjectOfType<TrainingManager>().GenEnd();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //check if occurs in the training scene
        if (FindObjectOfType<TrainingManager>() && 
            (collision.tag == "Pipe" ||  collision.tag == "Finish"))
            TrainedBirdDeath();

        //if occurs in game scene the death is the same as player 
        else
            base.OnTriggerEnter2D(collision);
    }


    
}
