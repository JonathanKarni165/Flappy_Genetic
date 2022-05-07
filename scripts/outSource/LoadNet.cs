using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawns birds with neural networks from saved txt files
/// </summary>
public class LoadNet : MonoBehaviour
{
    [SerializeField]
    public int[] netsIndexes;
    public GameObject bird;
    
    void Start()
    {
        for (int i = 0; i < netsIndexes.Length; i++)
        {
            NetSerialize.SetLoadPath(netsIndexes[i]);
            NeuralNetwork net = NetSerialize.Deserialize();

            GameObject instance = Instantiate(bird, transform.position, transform.rotation);
            instance.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            instance.GetComponent<FlappyNeural>().InitNet(net);
        }
    }
}
