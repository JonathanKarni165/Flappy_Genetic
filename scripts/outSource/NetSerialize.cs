using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// serialize a neural network into a new text file
/// </summary>
public class NetSerialize 
{
    public static string pathS = @"C:\Users\user\Desktop\Code\Nets\net.txt";
    public static string pathL = @"C:\Users\user\Desktop\Code\Nets\Cach\net.txt";

    /// <summary>
    /// set file path with gen index
    /// call to serialize after this function to serialize in a new file
    /// call to deserialize after this function to load net from memory
    /// </summary>
    /// <param name="index"></param>
    public static void SetStorePath(int index)
    {
        pathS = @"C:\Users\user\Desktop\Code\Nets\net" + index + ".txt";
    }

    public static void SetLoadPath(int index)
    {
        pathL = @"C:\Users\user\Desktop\Code\Nets\Cach\net" + index + ".txt";
    }

    /// <summary>
    /// save and serialize neural net object to txt file
    /// </summary>
    /// <param name="net"></param>
    public static void Serialize(NeuralNetwork net, int gen)
    {
        StreamWriter writer = new StreamWriter(pathS);

        string netSet = "";
        for (int i = 0; i < net.layerNum; i++)
        {
            netSet += net.layers[i].size + ",";
        }
        netSet += ";";

        writer.WriteLine(netSet);

        string biases = "";
        for (int i = 0; i < net.layerNum; i++)
        {
            biases += net.layers[i].bias + ",";
        }
        biases += ";";

        writer.WriteLine(biases);

        for (int i = 0; i < net.layerNum; i++)
        {
            for (int j = 0; j < net.layers[i].size; j++)
            {
                string neuron = i + "." + j + ":";
                for (int k = 0; k < net.layers[i].neurons[j].size; k++)
                {
                    neuron += net.layers[i].neurons[j].weights[k] + ",";
                }
                neuron += ";";
                writer.WriteLine(neuron);
            }
        }
        writer.Write("end \ngen: " + gen);
        writer.Close();
    }

    /// <summary>
    /// load neural net from txt file
    /// </summary>
    /// <returns> saved neural net from txt file </returns>
    public static NeuralNetwork Deserialize()
    {
        NeuralNetwork netOut;
        StreamReader read = new StreamReader(pathL);

        //read one line from file
        string line = read.ReadLine();
        List<int> netSet = new List<int>();

        //load layer set
        char[] seperator = { ',' };
        string[] sizes = line.Split(seperator);
        for (int i = 0; i < sizes.Length-1; i++)
        {
            netSet.Add(int.Parse(sizes[i]));
        }

        int[] netSetArr = netSet.ToArray();
        netOut = new NeuralNetwork(netSetArr);
        netOut.path = pathL;

        //biases line
        line = read.ReadLine();

        string[] biases = line.Split(seperator);

        for (int i = 0; i < netOut.layerNum; i++)
            netOut.layers[i].bias = float.Parse(biases[i]);

        line = read.ReadLine();
        while (!line.Contains("end"))
        {
            char[] cArr = line.ToCharArray();
            //find neuron index
            
            
            //seperate to neuron indexes and weights
            char[] seperator1 = { ':' };
            char[] seperator2 = { ',' };
            char[] seperator3 = { '.' };

            //split line
            string[] lineArr = line.Split(seperator1);
            //find neuron location
            string[] neuronIndexArr = lineArr[0].Split(seperator3);
            int layer = (int.Parse(neuronIndexArr[0])), neuron = (int.Parse(neuronIndexArr[1]));

            //split weights with ','
            string[] weightsArr = lineArr[1].Split(seperator2);

            //copy each weight value
            for (int i = 0; i < netOut.layers[layer].neurons[neuron].size; i++)
            {
                netOut.layers[layer].neurons[neuron].weights[i] = float.Parse(weightsArr[i]);
            }

            line = read.ReadLine();
        }

        return netOut;
    }

    /// <summary>
    /// load neural net from txt file
    /// </summary>
    /// <returns> saved neural net from txt file </returns>
    public static NeuralNetwork Deserialize(TextAsset netTxt)
    {
        NeuralNetwork netOut;
        int lineIndex = 2;
        char[] seperator = { '\n' };
        string[] lines = netTxt.text.Split(seperator);

        //read one line from file
        string line = lines[0];
        List<int> netSet = new List<int>();

        //load layer set
        seperator[0] = ',';
        string[] sizes = line.Split(seperator);
        for (int i = 0; i < sizes.Length - 1; i++)
        {
            netSet.Add(int.Parse(sizes[i]));
        }

        int[] netSetArr = netSet.ToArray();
        netOut = new NeuralNetwork(netSetArr);
        netOut.path = pathL;

        //biases line
        line = lines[1];

        string[] biases = line.Split(seperator);

        for (int i = 0; i < netOut.layerNum; i++)
            netOut.layers[i].bias = float.Parse(biases[i]);

        line = lines[lineIndex++];
        while (!line.Contains("end"))
        {
            char[] cArr = line.ToCharArray();
            //find neuron index


            //seperate to neuron indexes and weights
            char[] seperator1 = { ':' };
            char[] seperator2 = { ',' };
            char[] seperator3 = { '.' };

            //split line
            string[] lineArr = line.Split(seperator1);
            //find neuron location
            string[] neuronIndexArr = lineArr[0].Split(seperator3);
            int layer = (int.Parse(neuronIndexArr[0])), neuron = (int.Parse(neuronIndexArr[1]));

            //split weights with ','
            string[] weightsArr = lineArr[1].Split(seperator2);

            //copy each weight value
            for (int i = 0; i < netOut.layers[layer].neurons[neuron].size; i++)
            {
                netOut.layers[layer].neurons[neuron].weights[i] = float.Parse(weightsArr[i]);
            }

            line = lines[lineIndex++];
        }

        return netOut;
    }
}
