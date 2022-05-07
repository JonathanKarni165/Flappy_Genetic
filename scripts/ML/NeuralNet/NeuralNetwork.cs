using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeuralNetwork : IComparable<NeuralNetwork>
{
    //serial number helper 
    private static int counter = 0;

    public int serialNumber;

    //if not null is set to the net text serialized file
    public string path;

    //the total survivel time
    public float fitness;
    //how many pipes passed
    public int score;

    public Layer[] layers;
    private Layer inputLayer;
    //how many layers the net containes
    public int layerNum;
    
    /// <summary>
    /// build new neural network with random weight values
    /// </summary>
    /// <param name="layerSet"> representing each layer size </param>
    public NeuralNetwork(int[] layerSet)
    {
        this.serialNumber = counter;
        counter++;

        this.fitness = 0;
        this.score = 0;
        this.layerNum = layerSet.Length;
        
        this.layers = new Layer[layerNum];
        for (int i = 0; i < layerNum -1; i++)
        {
            this.layers[i] = new Layer(layerSet[i], layerSet[i + 1]);
        }
        this.layers[layerNum - 1] = new Layer(layerSet[layerNum - 1], 0);
        this.inputLayer = layers[0];
    }

    /// <summary>
    /// deep copy of neural network
    /// </summary>
    /// <param name="other"></param>
    public NeuralNetwork(NeuralNetwork other)
    {
        this.serialNumber = other.serialNumber;
        this.path = other.path;
        this.fitness = other.fitness;
        this.score = other.score;
        this.layerNum = other.layerNum;

        this.layers = new Layer[layerNum];
        for (int i = 0; i < layerNum; i++)
        {
            this.layers[i] = new Layer(other.layers[i]);
        }
        this.inputLayer = layers[0];
    }

    public override string ToString()
    {
        string output = "*Neural Net* " + this.serialNumber +"\nsize - " + this.layerNum + "\nfitness - " + this.fitness + "\n";

        for (int i = 0; i < this.layerNum; i++)
        {
            output += this.layers[i] + ",\n";
        }

        return output;
    }

    /// <summary>
    /// give the neural network initial input values
    /// </summary>
    /// <param name="inputArr"> the input of the net: deltaX, velocityY... </param>
    public void FeedInput(float[] inputArr)
    {
        for (int i=0; i<layers[0].size; i++)
        {
            this.inputLayer.neurons[i].val = inputArr[i];
        }
    }

    /// <summary>
    /// calculate each neuron value using the formula: y = w1*x1 + w2*x2... + b
    /// </summary>
    public void FeedForward()
    {
        //cycling through each layer
        for (int layer = 1; layer < this.layerNum; layer++)
        {
            Layer currentLayer = this.layers[layer];
            Layer lastLayer = this.layers[layer - 1];

            //cycling through each neuron
            for (int neuron = 0; neuron < currentLayer.size; neuron++)
            {
                Neuron currentNeuron = currentLayer.neurons[neuron];
                //sum of weights and inputs
                float value = currentLayer.bias;

                for(int weight = 0; weight < lastLayer.size; weight++)
                {
                    value += lastLayer.neurons[weight].val * lastLayer.neurons[weight].weights[neuron];
                }

                currentNeuron.val = (float)Math.Tanh(value);
            }
        }
    }

    /// <summary>
    /// based on chance the function will change or not each weight in the net
    /// </summary>
    public void Mutate()
    {
        float randomNumber;

        //cycling through each layer
        for (int layer = 0; layer < this.layerNum; layer++)
        {
            Layer currentLayer = this.layers[layer];

            //mutate bias value
            randomNumber = UnityEngine.Random.Range(0f, 50f);

            if (randomNumber <= 2f)
            { //if 1
              //flip sign of bias
                currentLayer.bias *= -1f;
            }
            else if (randomNumber <= 4f)
            { //if 2
              //pick random weight between -10 and 10
                currentLayer.bias = UnityEngine.Random.Range(-1f, 1f);
            }
            else if (randomNumber <= 6f)
            { //if 3
              //randomly increase by 0% to 100%
                float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                currentLayer.bias *= factor;
            }
            else if (randomNumber <= 8f)
            { //if 4
              //randomly decrease by 0% to 100%
                float factor = UnityEngine.Random.Range(0f, 1f);
                currentLayer.bias *= factor;
            }

            //cycling through each neuron
            for (int neuron = 0; neuron < currentLayer.size; neuron++)
            {
                Neuron currentNeuron = currentLayer.neurons[neuron];

                //cycling through each weight
                for(int weight = 0; weight < currentNeuron.size; weight++)
                {
                    //mutate weight value 
                    randomNumber = UnityEngine.Random.Range(0f, 50f);

                    if (randomNumber <= 2f)
                    { //if 1
                      //flip sign of weight
                        currentNeuron.weights[weight] *= -1f;
                    }
                    else if (randomNumber <= 4f)
                    { //if 2
                      //pick random weight between -1 and 1
                        currentNeuron.weights[weight] = UnityEngine.Random.Range(-0.5f, 0.5f);
                    }
                    else if (randomNumber <= 6f)
                    { //if 3
                      //randomly increase by 0% to 100%
                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                        currentNeuron.weights[weight] *= factor;
                    }
                    else if (randomNumber <= 8f)
                    { //if 4
                      //randomly decrease by 0% to 100%
                        float factor = UnityEngine.Random.Range(0f, 1f);
                        currentNeuron.weights[weight] *= factor;
                    }
                }
            }
        }
    }

    /// <summary>
    /// get the boolean output of the net jump or not
    /// </summary>
    /// <returns> return true if the last neuron value is greater then 0.6</returns>
    public bool GetOutput()
    {
        float result = this.layers[this.layerNum - 1].neurons[0].val;

        return result > 0.6;
    }

    /// <summary>
    /// Compare two neural networks and sort based on fitness
    /// </summary>
    /// <param name="other">Network to be compared to</param>
    /// <returns></returns>
    public int CompareTo(NeuralNetwork other)
    {
        if (other == null) return 1;

        if (fitness > other.fitness)
            return 1;
        else if (fitness < other.fitness)
            return -1;
        else
            return 0;
    }
}
