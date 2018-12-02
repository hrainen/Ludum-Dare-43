using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demon : MonoBehaviour {

    public GameObject animalToSacrifice = null;
    public GameObject player;
    public int sacrificeTime = 3;
    private IEnumerator coroutine;
	
	// Update is called once per frame
	void Update () {
		if ( animalToSacrifice != null && animalToSacrifice.CompareTag("Animal") )
        {
            sacrifice( animalToSacrifice );
        }
	}

    public void sacrifice( GameObject animal )
    {
        // get powerup name
        string powerName = animal.GetComponent<animal>().powerUpName;

        // get powerup value
        float powerValue = animal.GetComponent<animal>().powerUpValue;

        Destroy( animal, sacrificeTime ); // this is asynchronus
        coroutine = grantPowerUp( powerName, powerValue );
        StartCoroutine( coroutine );

        animalToSacrifice = null;
    }

    private IEnumerator grantPowerUp( string powerUp, float value )
    {
        yield return new WaitForSeconds(sacrificeTime);
        // check if player already has powerUp
        if ( player.GetComponent<player>().hasPowerup )
        {
            player.GetComponent<player>().removePowerUp();
        }

        player.GetComponent<player>().addPowerUp( powerUp, value );
    }
}
