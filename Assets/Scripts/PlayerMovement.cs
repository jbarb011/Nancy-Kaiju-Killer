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
                    
    public float kb_timer;                     //how long character should be knocked back for
    public float kb_duration = 0.5f;                   // where value is kept

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

        //MOVEMENT
        if (kb_timer <= 0)
        {
            moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);


            // Direction character is facing
            if (moveX < 0.0f && facingRight == true) FlipPlayer();
            else if (moveX > 0.0f && facingRight == false) FlipPlayer();

            //Jump controller
            if (Input.GetButtonDown("Jump")) Jump();
        }
        // If hit, lose control of movement for a second
        else
        {
            kb_timer -= Time.deltaTime;
        }



        //CUSTOM GRAVITY
        if (rb.velocity.y < 2.5) rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 2.5 && !Input.GetButton("Jump")) rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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

    public void Player_Hit()
    {
        kb_timer = kb_duration;
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
