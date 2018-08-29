using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public int damage_output = 5;           //Damage Output to Player
    public float kb_amount = 200f;          //Amount of Kickback
    public Collider2D hittrigger;
    public GameObject source;               //Reference back to source

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
