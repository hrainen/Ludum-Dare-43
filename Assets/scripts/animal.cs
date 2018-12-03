using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal : MonoBehaviour {

    public bool isBeingHeld = false;
    public Transform playerHoldTarget;
    public bool isBeingSacrificed = false;
    public Transform sacrificeSpot;
    public bool canBePickedUp = true;

    public float health = 100;

    // ========== PowerUp Info ============ //
    // TO DO: MAKE THIS SCRIPTABLE OBJECT   //

    public string powerUpName = "speed";
    public float powerUpValue = 1.5f;

    // ============== particle effects ========== //
    public GameObject bloodEffect;

    // Update is called once per frame
    void Update () {

		if ( isBeingHeld )
        {
            transform.position = new Vector3 ( playerHoldTarget.position.x, playerHoldTarget.position.y, -3);
        }

        if ( isBeingSacrificed )
        {
            transform.position = new Vector3(sacrificeSpot.position.x, sacrificeSpot.position.y, -1.5f);
        }
        
        if ( health <= 0)
        {
            Invoke("die", .2f);
        }

    }

    public void toggleAll()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();

        for (int i = 0; i < colliders.Length ; i++)
        {
            colliders[i].enabled = !colliders[i].enabled;
        }
    }

    public void sacrifice( )
    {
        isBeingSacrificed = true;
        canBePickedUp = false;
    }

    public bool pickUp ()
    {
        if ( canBePickedUp )
        {
            isBeingHeld = true;
            canBePickedUp = false;
            toggleAll();
            return true; // successfully pickedup
        }

        return false; // could not pick up
    }

    public void drop()
    {
        isBeingHeld = false;
        canBePickedUp = true;
        toggleAll();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Lethal"))
        {
            health = 0;
        }
    }

    public void die()
    {
        // disable any movement

        // spawn particle system
        Instantiate( bloodEffect, transform.position, Quaternion.identity );

        // destroy self
        Destroy(gameObject);
    }

    public void takeDmg( float dmg )
    {
        health -= dmg;
    }

}
