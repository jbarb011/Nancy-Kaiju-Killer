using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileTracking : MonoBehaviour {

    public float time_between_shots = 2f;
    public float bullet_destroytime = 5.0f;
    public bool player_inArea;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    GameObject player;
    float timer;

	// Use this for initialization
	void Start () {
        player_inArea = false;
	}
	
	// Update is called once per frame
	void Update () {


        if (player_inArea)
        {
            transform.right = player.transform.position - transform.position;

            if (timer <= 0)
            {
                shoot();
                timer = time_between_shots;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
	}

    void shoot()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 15;

        Destroy(bullet, bullet_destroytime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            player_inArea = true;

            timer = time_between_shots;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
            player_inArea = false;

            timer = 0;
        }
    }
}
