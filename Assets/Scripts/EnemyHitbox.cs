using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour {

    public int Health = 10;

    GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		if(Health <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerMelee")
        {
            Health -= collision.gameObject.GetComponent<PlayerDamage>().damage_output;
        }
    }
}
