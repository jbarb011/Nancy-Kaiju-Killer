using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int Health = 100;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
        
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
                //get kb and position of source of damage
                float kb_amount = collision.GetComponent<PlayerDamage>().kb_output;

                if(kb_amount > 0)
                {
                    GameObject source = collision.GetComponent<PlayerDamage>().source;

                    //kickback in whichever direction they came from
                    if (source.GetComponent<Transform>().localPosition.x < gameObject.GetComponent<Transform>().localPosition.x)
                    {
                        rb.AddForce(new Vector2(kb_amount, 0));
                    }
                    else
                    {
                        rb.AddForce(new Vector2(-kb_amount, 0));
                    }
                }

            }
        }
    }
}
