using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Neuron
{
    public float val;
    public int size;
    public float[] weights;

    /// <summary>
    /// neuron holds a value and several weights (edges to next layer neurons)
    /// </summary>
    /// <param name="len"> the number of weights </param>
    public Neuron(int len)
    {
        this.size = len;
        this.val = 0;
        this.weights = new float[len];
        for (int i = 0; i < len; i++)
        {
            this.weights[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    /// <summary>
    /// deep copy 
    /// </summary>
    /// <param name="other"></param>
    public Neuron(Neuron other)
    {
        this.size = other.size;
        this.val = 0;
        this.weights = new float[this.size];
        for (int i = 0; i < size; i++)
        {
            this.weights[i] = other.weights[i];
        }
    }

    public override string ToString()
    {
        string output = "[*" + this.val + "* ";
        for (int i = 0; i < this.weights.Length; i++)
        {
            output += ", " + this.weights[i].ToString("0.00");
        }
        output += "]";

        return output;
    }

}
