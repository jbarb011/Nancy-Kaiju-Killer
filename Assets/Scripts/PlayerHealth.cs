using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int Player_Health = 100;             //Amount of Health Player has
    

    public BoxCollider2D player_box;            //Player hitbox
    public Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if you come in contact with ENEMY
        if(collision.gameObject.tag == "Enemy")
        {
            // Subtract health
            Player_Health -= collision.gameObject.GetComponent<EnemyDamage>().damage_output;

            // If Health is out then die
            if (Player_Health <= 0)
            {
                Destroy(gameObject);
            }
            // Else knockback
            else
            {
                //get kb and position of source of damage
                float kb_amount = collision.gameObject.GetComponent<EnemyDamage>().kb_amount;
                GameObject source = collision.gameObject.GetComponent<EnemyDamage>().source;

                //kickback in whichever direction they came from
                if (source.GetComponent<Transform>().localPosition.x < gameObject.GetComponent<Transform>().localPosition.x)
                {
                    rb.AddForce(new Vector2(kb_amount, 0));
                }
                else
                {
                    rb.AddForce(new Vector2(-kb_amount, 0));
                }

                gameObject.GetComponent<PlayerMovement>().Player_Hit();
            }
        }
    }
}
