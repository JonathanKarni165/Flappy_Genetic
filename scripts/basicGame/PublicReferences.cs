using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// helper class stores public references to some objects
/// </summary>
public class PublicReferences : MonoBehaviour
{
    public TrainingManager tm;
    public static TrainingManager trainingManager;

    //collector and collectibles spawner set and check values 
    public static bool speedCollectiblesAllowed;

    public Camera playerCm, botCm;
    public static Camera playerCam, botCam;
    
    private void Awake()
    {
        trainingManager = tm;
        speedCollectiblesAllowed = true;
        playerCam = playerCm;
        botCam = botCm;
    }

}
