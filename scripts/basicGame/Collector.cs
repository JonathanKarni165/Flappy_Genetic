using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script for collecting the game collectibles
/// </summary>
public class Collector : MonoBehaviour
{
    public PipeSpawner myPipSpwn, enemyPipSpwn;
    public GameObject laser, shootPoint;
    public FlappyInputController flappyControl;
    public Image fastIcon, slowIcon;

    //called when colliding with a collectible
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit from laser/poop
        if (this.tag == "Player" && collision.tag == "Laser")
            flappyControl.Hit();
        if (this.tag == "Bot" && collision.tag == "Poop")
            flappyControl.Hit();

        //collect 
        char[] sep = { '_' };
        string[] args = collision.tag.Split(sep);

        //not a collectible, return
        if (!args[0].Equals("collect"))
            return;

        //collectible number 0-2
        int colNum = int.Parse(args[1]);
        
        switch (colNum)
        {
            case 0:
                StartCoroutine("FastSpeed");
                break;
            case 1:
                StartCoroutine("SlowSpeed");
                break;
            case 2:
                ShootLaser();
                break;
        }

        Destroy(collision.gameObject);
    }

    //shoot laser or poop after collecting laser reward
    public void ShootLaser()
    {
        Instantiate(laser.transform, shootPoint.transform.position, laser.transform.rotation).
            GetComponent<BulletMovement>().speed = myPipSpwn.speed;
    }

    public void DestroyAllSpeedCollectibles()
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("collect_0");
        foreach (GameObject gm in arr)
            Destroy(gm);
        arr = GameObject.FindGameObjectsWithTag("collect_1");
        foreach (GameObject gm in arr)
            Destroy(gm);
    }

    IEnumerator FastSpeed()
    {
        DestroyAllSpeedCollectibles();
        fastIcon.enabled = true;
        PublicReferences.speedCollectiblesAllowed = false;

        enemyPipSpwn.IncreaseSpeed(1f);
        yield return new WaitForSeconds(7f);
        enemyPipSpwn.IncreaseSpeed(1f);
        yield return new WaitForSeconds(10f);
        enemyPipSpwn.IncreaseSpeed(-1f);
        yield return new WaitForSeconds(7f);
        enemyPipSpwn.IncreaseSpeed(-1f);

        PublicReferences.speedCollectiblesAllowed = true;
        fastIcon.enabled = false;
    }

    IEnumerator SlowSpeed()
    {
        DestroyAllSpeedCollectibles();
        slowIcon.enabled = true;
        PublicReferences.speedCollectiblesAllowed = false;

        myPipSpwn.IncreaseSpeed(-1f);
        yield return new WaitForSeconds(15f);
        myPipSpwn.IncreaseSpeed(1f);

        PublicReferences.speedCollectiblesAllowed = true;
        slowIcon.enabled = false;
    }
}
