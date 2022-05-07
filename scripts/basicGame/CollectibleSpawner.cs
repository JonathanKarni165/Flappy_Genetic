using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawnes collectibles 
/// </summary>
public class CollectibleSpawner : MonoBehaviour
{
    //each pipe has its own spawn range in front of it
    public Collider2D spawnRange;

    //all the collectible prefabs
    public GameObject[] collectibles;

    //the chances to spawn 
    //if 3 then 1 out of 3 attempts will succeed
    public int spawnChance;

    private void OnEnable()
    {
        int randNum = Random.Range(1, spawnChance);
        if (randNum == 1 && !FindObjectOfType<TrainingManager>())
        {
            Vector2 spwnPos = new Vector2(Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
                Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y));

            int randIndex = Random.Range(0, collectibles.Length);

            //check if allowed to spawn this type of collectible
            if (randIndex <= 1 && !PublicReferences.speedCollectiblesAllowed)
                return;

            Instantiate(collectibles[randIndex], spwnPos, transform.rotation, transform);
        }
    }
}
