using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int Player_Health = 100;

    public BoxCollider2D player_box;            //Player hitbox
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if you come in contact with Enemy
        if(collision.gameObject.tag == "Enemy")
        {
            
        }
    }
}
