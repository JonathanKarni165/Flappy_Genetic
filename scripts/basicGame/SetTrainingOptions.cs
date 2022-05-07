using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// manage the set options prompt in the training scene
/// </summary>
public class SetTrainingOptions : MonoBehaviour
{
    //scripts references
    public PipeSpawner ps;
    public BirdSpawner bs;
    public TrainingManager tm;

    public Slider[] sliders;
    public TMP_InputField inputText;
    public GameObject panel;

    public void OnGoButtonPressed()
    {
        bs = FindObjectOfType<BirdSpawner>();
        ps = FindObjectOfType<PipeSpawner>();

        //change hole size
        GameObject instance = ps.pipe;

        instance.GetComponent<PipeSetup>().hole.transform.localScale =
            new Vector2(1, sliders[0].value);

        ps.pipe = instance;

        //change pipe speed
        ps.IncreaseSpeed(sliders[1].value - ps.speed);

        //change population
        BirdSpawner.population = (int)sliders[2].value;

        //change changerate
        tm.changeRate = (int)(BirdSpawner.population * sliders[3].value);

        //change exel path
        if (inputText.text.Length > 0)
        {
            ExelFormat.SetPath(inputText.text);
            ExelFormat.isPathSet = true;
        }

        panel.SetActive(false);
        tm.StartGeneration();
    }

}
