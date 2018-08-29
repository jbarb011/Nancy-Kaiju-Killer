using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int Health = 100;

    public float kb_amount = 50f;
    public float kb_timer_amount = 0.2f;
    public float kb_timer;

    GameObject Player;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        kb_timer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        // Knockback
		if(kb_timer > 0)
        {
            if (Player.GetComponent<Transform>().localPosition.x < gameObject.GetComponent<Transform>().localPosition.x)
            {
                rb.AddForce(new Vector2(kb_amount, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-kb_amount, 0));
            }
            kb_timer -= Time.deltaTime;
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit by player punch
        if(collision.tag == "PlayerAttack")
        {
            // Subtract health
            Health -= collision.gameObject.GetComponent<PlayerDamage>().damage_output;

            // If Health is out then die
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
            // Else knockback
            else
            {
                //set timer
                kb_timer = kb_timer_amount;
            }
        }
    }
}
