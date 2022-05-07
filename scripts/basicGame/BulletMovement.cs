using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// movement script for bullet (laser or poop prefabs)
/// </summary>
public class BulletMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int direction;

    //determine if the player shot it or the bot
    public bool isPlayer;

    //both players cameras
    Camera playerCam, botCam;

    [HideInInspector]
    public float speed;
  
    void Start()
    {
        rb.velocity = new Vector2(direction * speed, 0);
        playerCam = PublicReferences.playerCam;
        botCam = PublicReferences.botCam;

        //destroy bullet when out of screen
        Destroy(gameObject, 13);
    }

    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, 0);

        //check if passed this player's screen 
        //and teleport to enemy screen
        if (transform.position.x < (playerCam.transform.position.x - playerCam.orthographicSize) && isPlayer)
        {
            transform.position = new Vector2(botCam.transform.position.x + (botCam.orthographicSize),
                botCam.transform.position.y + (transform.position.y - playerCam.transform.position.y));
        }

        if (transform.position.x > (botCam.transform.position.x + botCam.orthographicSize) && !isPlayer)
        {
            transform.position = new Vector2(playerCam.transform.position.x - (playerCam.orthographicSize),
                playerCam.transform.position.y + (transform.position.y - botCam.transform.position.y));
        }
    }

}
