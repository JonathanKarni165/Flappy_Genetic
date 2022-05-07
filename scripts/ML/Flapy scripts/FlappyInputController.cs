using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// movement script for a bird controlled by the player
/// </summary>
public class FlappyInputController : MonoBehaviour
{
    //how many pipes passed
    public int score;

    //reference to physics component
    protected Rigidbody2D rb;
    
    public float jumpSpeed;

    //set to false in children
    public bool isPlayer;

    public bool canDie;

    //does it have animation
    public bool isAnimate;
    //reference animation componenet
    public FlapAnim fa;

    //reference to hearts UI
    public Image[] hearts;

    //used for respawning after a hit was taken
    public Transform startPosition;

    //invinsibility for a short amount of time after a hit was taken
    protected int hitpoints;
    protected float hitCoolDown, maxHitCoolDown = 1f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        score = 0;

        //initialize health to 3 hitpoints (3 hearts on screen)
        hitpoints = 3;
    }

    void Update()
    {
        //the bird will never move on the x axis
        rb.velocity = new Vector2(0, rb.velocity.y);

        //input for jumping 
        if(Input.GetKeyDown(KeyCode.Space) && isPlayer)
            Jump();

        //decerementing from the invinsibility duration
        hitCoolDown -= Time.deltaTime;
    }

    public void Jump()
    {
        //add speed on the y axis
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        if (isAnimate)
            fa.Flap();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //passed a pipe's hole
        if (collision.gameObject.tag == "pipeFlag")
            score += 1;

        //hit a pipe
        if (collision.gameObject.tag == "Pipe")
            Hit();

        //hit the floor/ceiling
        if (collision.gameObject.tag == "Finish")
        {
            Hit();
            transform.position = startPosition.position;
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void Hit()
    {
        //set hit animation
        if (isAnimate)
            fa.Blip();

        //prevent double trigger
        if (hitCoolDown > 0)
            return;

        hitCoolDown = maxHitCoolDown;

        //decrement from bird health
        hitpoints--;
        //update UI
        hearts[hitpoints].enabled = false;

        //check if dead
        if (hitpoints == 0)
        {
            //end of the game
            gameObject.SetActive(false);
            
            if (gameObject.tag == "Player")
                FindObjectOfType<UIManager>().SetPanel("YOU LOST");
            else
                FindObjectOfType<UIManager>().SetPanel("YOU WON!");
        }
    }
}
