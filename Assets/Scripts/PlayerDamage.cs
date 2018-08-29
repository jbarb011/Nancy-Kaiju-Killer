using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    public int damage_output = 10;          //Damage Output to Enemy
    public bool hitting = false;            //whether or not attack is done
    public float hit_timer = 0f;            //where attackbox duration is kept
    public float hit_cooldown = 0.3f;       //attackbox duration

    public Collider2D hittrigger;           //Locate attackbox used

    private void Awake()
    {
        // set collider to off
        hittrigger.enabled = false;
    }

    private void Update()
    {
        //  get if button is down and not already hitting
        if (Input.GetButtonDown("Z") && !hitting)
        {
            // set everything up
            hittrigger.enabled = true;
            hit_timer = hit_cooldown;
            hitting = true;
        }

        // if hitting
        if (hitting)
        {
            // subtract timer
            if(hit_timer > 0)
            {
                hit_timer -= Time.deltaTime;
            }
            // if timers up reset everything
            else
            {
                hitting = false;
                hittrigger.enabled = false;
            }
        }
    }
    
}
