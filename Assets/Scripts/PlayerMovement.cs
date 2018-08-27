using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool facingRight = true;             //Sprite Direction

    public int playerSpeed = 10;                //X-axis speed
    public float moveX;                         //X-axis movement

    public float playerJumpMin = 500f;          //Minimum Ground Jump Speed
    public float playerJumpMax = 1500f;         //Maximum Ground Jump Speed
    public float TimeCharge = 0f;               //Timer for Charging Jump
    public float ChargeMultiplier = 3000f;      //How fast Charging Jump accumulates

    public float AirJumpSpeed = 100f;           //Jump for when in air
    public int NumAirJumps = 2;              // Number of air jumps you can make in game
    public int AirJumps;                        // Number of air jumps currently available
    public bool inAir;                          // Check if not on Ground

    Rigidbody2D rb;

    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {

        //INPUT
        moveX = Input.GetAxis("Horizontal");


        //JUMPING
        if (!inAir)
        {
            //CALCULATE TIME OF INPUT JUMP
            if (Input.GetButton("Jump")) TimeCharge += Time.deltaTime;
            //WHEN SPACE IS LET GO, JUMP
            if (Input.GetButtonUp("Jump")) ChargeJump();
        }
        else
        {
            //If In Air only do NumAirJumps amount of times
            if (Input.GetButtonDown("Jump") && AirJumps > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * AirJumpSpeed);
                AirJumps--;
                Debug.Log(AirJumps);
            }
        }



        //DIRECTION
        if (moveX < 0.0f && facingRight == true) FlipPlayer();
        else if (moveX > 0.0f && facingRight == false) FlipPlayer();



        //PHYSICS
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        Debug.Log(moveX * playerSpeed);
	}

    void ChargeJump()
    {
        //CALCULATE JUMP CHARGE
        float JumpCharge = (TimeCharge*ChargeMultiplier) + playerJumpMin;



        //IF JUMP CHARGE IS LESS THAN MAX JUMP DO JUMP CHARGE
        //ELSE DO MAX JUMP
        if (JumpCharge < playerJumpMax)
        {
            rb.AddForce(Vector2.up * JumpCharge);
        }
        else
        {
            rb.AddForce(Vector2.up * playerJumpMax);
        }

        //RESTART TIMECHARGE
        TimeCharge = 0f;
    }

    void FlipPlayer()
    {
        //FLIP SPRITE OF CHARACTER
        facingRight = !facingRight; 
        Vector3 curr_scale = gameObject.GetComponent<Transform>().localScale;
        gameObject.GetComponent<Transform>().localScale = new Vector3(-curr_scale.x, curr_scale.y, curr_scale.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            inAir = false;
            AirJumps = NumAirJumps;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            inAir = true;
        }
    }
}
