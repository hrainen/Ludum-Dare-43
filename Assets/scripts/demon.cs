using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demon : MonoBehaviour {

    public GameObject animalToSacrifice = null;
    public GameObject player;
    public int sacrificeTime = 3;
    private IEnumerator coroutine;
    public GameObject UIMngr;
	
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

        coroutine = grantPowerUp( powerName, powerValue, animal );
        StartCoroutine( coroutine );

        animalToSacrifice = null;
    }

    private IEnumerator grantPowerUp( string powerUp, float value, GameObject animal )
    {
        yield return new WaitForSeconds(sacrificeTime);

        animal.GetComponent<animal>().takeDmg(100); // kill animal

        // check if player already has powerUp
        if ( player.GetComponent<player>().hasPowerup )
        {
            player.GetComponent<player>().removePowerUp();
        }

        player.GetComponent<player>().addPowerUp( powerUp, value );

        // display powerup on UI
        if ( animal.GetComponent<animal>().powerUpName == "speed" )
        {
            UIMngr.GetComponent<UI_Manager>().showSpeedPowerUp();
        }

        if (animal.GetComponent<animal>().powerUpName == "jump")
        {
            UIMngr.GetComponent<UI_Manager>().showJumpPowerUp();
        }

    }
}
