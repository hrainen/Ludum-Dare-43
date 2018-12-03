using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public GameObject bunny;
    public GameObject sheep;
    public GameObject speedPowerUp;
    public GameObject jumpPowerup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if player is holding animal, display animal
        
        // if player has powerup, display powerup
	}

    public void showBunny()
    {
        if ( sheep.activeSelf ) // if sheep is showing
        {
            sheep.SetActive( false );
        }

        bunny.SetActive( true );

    }


    public void showSheep()
    {
        if ( bunny.activeSelf ) // if sheep is showing
        {
            bunny.SetActive( false );
        }

        sheep.SetActive( true );
    }

    public void stopDisplayingAnimal()
    {
        if (sheep.activeSelf) // if sheep is showing
        {
            sheep.SetActive(false);
        }

        if (bunny.activeSelf) // if sheep is showing
        {
            bunny.SetActive(false);
        }

    }

    public void showSpeedPowerUp()
    {
        if (jumpPowerup.activeSelf) // if sheep is showing
        {
            jumpPowerup.SetActive(false);
        }

        speedPowerUp.SetActive(true);

    }


    public void showJumpPowerUp()
    {
        if (speedPowerUp.activeSelf) // if sheep is showing
        {
            speedPowerUp.SetActive(false);
        }

        jumpPowerup.SetActive(true);
    }

    public void stopDisplayingPowerUp()
    {
        if (jumpPowerup.activeSelf) // if sheep is showing
        {
            jumpPowerup.SetActive(false);
        }

        if (speedPowerUp.activeSelf) // if sheep is showing
        {
            speedPowerUp.SetActive(false);
        }

    }
}
