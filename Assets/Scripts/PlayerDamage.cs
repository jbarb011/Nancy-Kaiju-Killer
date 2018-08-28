using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    public int damage_output = 10;
    public bool hitting = false;
    public float hit_timer = 0f;
    public float hit_cooldown = 0.3f;

    public Collider2D hittrigger;

    private void Awake()
    {
        hittrigger.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Z") && !hitting)
        {
            hittrigger.enabled = true;
            hit_timer = hit_cooldown;
            hitting = true;
        }

        if (hitting)
        {
            if(hit_timer > 0)
            {
                hit_timer -= Time.deltaTime;
            }
            else
            {
                hitting = false;
                hittrigger.enabled = false;
            }
        }
    }
}
