using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool facingRight = true;             // Sprite Direction

    public int playerSpeed = 20;                // X-axis speed
    public float moveX;                         // X-axis movement

    public float groundJumpSpeed = 700f;       // Speed in which Player Jumps

    public float airJumpSpeed = 500f;          // Jump for when in air
    public int numAirJumps = 2;                 // Number of air jumps you can make in game
    public int airJumps;                        // Number of air jumps currently available
    public bool inAir;                          // Check if not on Ground

    public float fallMultiplier = 3f;         //Increased falling for peak jumps
    public float lowJumpMultiplier = 3f;        //Increased falling for low jumps

    Rigidbody2D rb;                             // Rigidbody for Player

    void Awake ()
    {
        //Get Rigidbody Component
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        airJumps = numAirJumps;
    }

	// Update is called once per frame
	void Update () {

        //INPUT
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) Jump();

        //CUSTOM GRAVITY
        if (rb.velocity.y < 2.5) rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 2.5 && !Input.GetButton("Jump")) rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;



        //DIRECTION
        if (moveX < 0.0f && facingRight == true) FlipPlayer();
        else if (moveX > 0.0f && facingRight == false) FlipPlayer();



        //LEFT AND RIGHT MOVEMENT
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
	}

    void Jump()
    {

        // Speed of jump depends on whether or not you are in air or not
        if (inAir && airJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * airJumpSpeed);

            // Subtract air jumps
            airJumps--;
        }
        else if (!inAir)
        {
            rb.AddForce(Vector2.up * groundJumpSpeed);
        }
        
    }

    void FlipPlayer()
    {
        // Flip sprite of character depending where they are moving
        facingRight = !facingRight; 
        Vector3 curr_scale = gameObject.GetComponent<Transform>().localScale;
        gameObject.GetComponent<Transform>().localScale = new Vector3(-curr_scale.x, curr_scale.y, curr_scale.z);
    }


    // Check if on ground or air
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            //Reset airjumps if landing
            if (inAir)
            {
                airJumps = numAirJumps;
                inAir = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!inAir)
            {
                inAir = true;
            }
        }
    }
}
