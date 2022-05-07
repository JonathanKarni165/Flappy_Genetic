using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// represent a single layer in the neural network
/// contains an array of neurons 
/// </summary>
[System.Serializable]
public class Layer
{
    //the bias is the offset added to the formula
    //y = w1*x1 ... + b (bias)
    public float bias;
    //how many neurons in the array
    public int size;
    public Neuron[] neurons;

    /// <summary>
    /// constructor to a new layer 
    /// </summary>
    /// <param name="bias"> b variable </param>
    /// <param name="size"> how many neurons in the layer </param>
    /// <param name="nextLayerSize"> the next layer size is necessary for determining 
    /// how many weights for each neuron in layer</param>
    public Layer(float bias, int size, int nextLayerSize)
    {
        this.bias = bias;
        this.size = size;

        this.neurons = new Neuron[size];
        for (int i = 0; i < size; i++)
        {
            this.neurons[i] = new Neuron(nextLayerSize);
        }
    }

    /// <summary>
    /// constructor to a new layer the bias is set randomly
    /// </summary>
    /// <param name="size"> how many neurons in the layer </param>
    /// <param name="nextLayerSize"> the next layer size is necessary for determining 
    /// how many weights for each neuron in layer</param>
    public Layer(int size, int nextLayerSize)
    {
        this.bias = Random.Range(-1f, 1f);
        this.size = size;

        this.neurons = new Neuron[size];
        for (int i = 0; i < size; i++)
        {
            this.neurons[i] = new Neuron(nextLayerSize);
        }
    }

    /// <summary>
    /// deep copy of new layer
    /// </summary>
    /// <param name="other"> other layer </param>
    public Layer(Layer other)
    {
        this.bias = other.bias;
        this.size = other.size;

        this.neurons = new Neuron[size];
        for (int i = 0; i < size; i++)
        {
            this.neurons[i] = new Neuron(other.neurons[i]);
        }
    }

    public override string ToString()
    {
        string output = "{ ";
        for (int i = 0; i < size; i++)
        {
            output += neurons[i] + ", ";
        }
        output += "[" + this.bias + "]" + " }";

        return output;
    }
}
