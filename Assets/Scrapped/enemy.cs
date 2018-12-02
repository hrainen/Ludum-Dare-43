using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public float health = 50;
    public float moveSpeed = 20;
    private float horizontalMovement = 0f;
    public GameObject bloodEffect;

	
	// Update is called once per frame
	void Update () {
		if ( health <= 0)
        {
            Destroy(gameObject);
        }

        horizontalMovement = moveSpeed * Time.deltaTime;
	}

    private void FixedUpdate()
    {
        transform.position = new Vector2( transform.position.x, transform.position.y) - new Vector2 ( horizontalMovement, 0 );
    }

    public void takeDamage( float dmg )
    {
        Vector3 bloodSpawn = new Vector3(transform.position.x, transform.position.y, -1);
        GameObject effect = Instantiate(bloodEffect, bloodSpawn, Quaternion.identity);
        Destroy(effect, 2); // destroy particle effect after 2 seconds
        health -= dmg;
    }


}
