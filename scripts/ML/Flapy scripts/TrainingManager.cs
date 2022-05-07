using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// management script for the training process
/// </summary>
public class TrainingManager : MonoBehaviour
{
    //several training options
    public bool generateFromBest, isStartOnPlay;

    //the generation number
    public static int genNum;

    //change rate of the genetic algorithm how many birds to erase each iteration
    public int changeRate;

    //the target score
    public int targetScore;

    //iterations limit
    public int iterations;

    //the current highest score 
    public int highScore;

    //put the high score on screen
    public TextMeshProUGUI scoreUI;

    //reference to the bird spawner script 
    public BirdSpawner bs;
    public PipeSpawner ps;

    //the current birds list
    //public static List<GameObject> birds;
    //the current birds net list
    public static List<NeuralNetwork> nets;
    //the cuurent most succesful neural network
    public NeuralNetwork bestNet;

    public GameObject panel;
    

    //called each new training scene iteration
    private void Awake()
    { 
        if (genNum == 0)
        {
            panel.SetActive(true);
        }
        else
        {
            StartGeneration();
        }
    }

    public void StartGeneration()
    {
        
        //increment generation
        genNum++;

        //open a new excel file if this is the first generation
        if (genNum == 1 && ExelFormat.isPathSet)
            ExelFormat.OpenCSVFile();

        //birds = new List<GameObject>();

        //check if we have already got a nets list
        //if we have nets we will spawn the new ones based on previous
        if (nets != null)
        {
            bs.Spawn(nets);
            print("spawn last nets");
        }
        else
        {
            nets = new List<NeuralNetwork>();
            if (generateFromBest)
                bs.Spawn(NetSerialize.Deserialize());
            else
                bs.Spawn();
        }

        //enable pipe spawner
        ps.run = true;

    }

    /// <summary>
    /// after a spawning a new generation this function is called 
    /// to collect the new neural nets
    /// </summary>
    public void InitNetsList()
    {
        foreach (FlappyNeural fp in FindObjectsOfType<FlappyNeural>())
        {
            nets.Add(fp.net);
        }
    }

    /// <summary>
    /// generate new nets list for next generation
    /// </summary>
    private void GenerateNewNets()
    {
        for(int i = 0; i < changeRate; i++)
        {
            //delete the worst nets
            nets.RemoveAt(i);

            //duplicate the best nets
            NeuralNetwork mutatedNet = new NeuralNetwork(nets[nets.Count - 1]);
            //and mutate them to discover even more succesful nets
            mutatedNet.Mutate();
            nets.Add(mutatedNet);
        }
    }

    /// <summary>
    /// run all generation ending functions
    /// </summary>
    public void GenEnd()
    {
        foreach(FlappyNeural bird in FindObjectsOfType<FlappyNeural>())
        {
            if (bird.gameObject.activeInHierarchy)
                return;
        }

        //sort nets by fitness
        nets.Sort();
        //get current generation best net
        NeuralNetwork best = nets[nets.Count - 1];
        print(nets.Count);
        bestNet = best;
        //if we get a new high score we will save a serialized text 
        //file representing the current net
        if (best.score > highScore) 
        {
            //update the high score and the best net 
            highScore = best.score;
            bestNet = best;
            
            //NetSerialize.Serialize(bestNet, genNum);
        }

        //update high score UI accordingly
        scoreUI.text = "High Score:" + highScore;

        //add new line to the excel file
        if (ExelFormat.isPathSet)
            ExelFormat.AddLine(genNum, best.score);
        
        //the genetic algorithm to generate new nets to next generation
        GenerateNewNets();
        
        //should wait a while to let the rest of the training process run
        StartCoroutine("Wait");
    }
    
    /// <summary>
    /// wait 1 sec before reloading
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
