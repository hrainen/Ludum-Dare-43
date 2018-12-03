using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public float health = 100;
    public float moveSpeed = 15;
    public float jumpForce = 10;


    private Rigidbody2D rb;
    private float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool isFacingRight = true;

    // other stats can go here
    private bool isHoldingAnimal = false;
    private bool canHoldAnimal = false;
    private GameObject animalToPickUp = null;
    private GameObject animalHolding = null;
    private float handleDelay = .3f; // delay when we can interact with animal
    private float timeToHandle = 0;

    // ============= Sacrifice Conditions ============== //
    private bool isAbleSacrifce = false;
    public GameObject Demon;

    // ============= Power Ups ============== //
    public bool hasPowerup = false;
    private float speedPowerup = 1; // if > 1 will have effect
    private float jumpPowerup = 1; // if > 1 will have effect

    private float speedLimit = 2;
    private float jumpLimit = 2;

    // ============= Exit ===================== // 

    public GameObject SceneMngr;

    // ============= particle effects ===========//
    public GameObject dustTrail;

    // ============= UI ================//
    public GameObject UIMngr;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        if ( health > 0 ) // if player is alive
        {
            if ( !isGrounded || Input.GetAxisRaw("Horizontal") == 0 )
            {
                // were not on ground or were not moving
                dustTrail.SetActive( false );
            }

            if ( isGrounded && Input.GetAxisRaw("Horizontal") != 0)
            {
                // were not on ground or were not moving
                dustTrail.SetActive( true );
            }

            // player jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce * jumpPowerup;
            }

            // pick up new animal
            if (canHoldAnimal && Input.GetMouseButtonDown(0) && animalToPickUp != null && timeToHandle <= 0 && !isHoldingAnimal)
            {
                // then pick up the animal
                pickUpAnimal(animalToPickUp);
            }

            // drop current animal
            if (isHoldingAnimal && Input.GetMouseButtonDown(0) && animalHolding != null && timeToHandle <= 0)
            {
                // if standing at demon, sacrifice animal
                if (isAbleSacrifce)
                {
                    sacrifice(animalHolding);
                }

                // drop the animal
                if (!isAbleSacrifce)
                {
                    dropAnimal(animalHolding);
                }

            }

            // handle animal timer
            if (timeToHandle > 0)
            {
                timeToHandle -= Time.deltaTime;
            }

            // TO DO: DISPLAY WHAT ANIMAL PLAYER IS HOLDING
        }

        else
        {
            // stop displaying player

            // activate blood particle system
        }

    }

    private void FixedUpdate()
    {
        if (health > 0) // if player is alive
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxisRaw("Horizontal");

            // move right
            if (moveInput > 0 && !isFacingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                isFacingRight = true;
            }

            // move left
            if (moveInput < 0 && isFacingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                isFacingRight = false;

            }

            rb.velocity = new Vector2(moveInput * moveSpeed * speedPowerup, rb.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( groundCheck.position, checkRadius);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // TO DO: REPLACE WITH SWITCH STATEMENT
        if (col.CompareTag("Animal"))
        {
            canHoldAnimal = true; // we are standing on top of animal
            animalToPickUp = col.gameObject;

        }

        if (col.CompareTag("Sacrifice"))
        {
            isAbleSacrifce = true; // we are standing on top of spot to sacrifice
        }

        if (col.CompareTag("Exit"))
        {
            SceneMngr.GetComponent<LevelSwitch>().readyToTransition();

        }

        if (col.CompareTag("Lethal"))
        {
            health = 0;
            Invoke( "die", 3 );
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Animal"))
        {
            canHoldAnimal = false;
            animalToPickUp = null;

        }

        if (col.CompareTag("Sacrifice"))
        {
            isAbleSacrifce = false; // we are leaving the spot to sacrifice
        }
    }

    private void dropAnimal( GameObject animal )
    {
        //Debug.Log("Dropping Animal!");

        animal.GetComponent<animal>().drop();

        // player is not holding animal anymore
        isHoldingAnimal = false;
        animalHolding = null;

        // reset timer for handling animal
        timeToHandle = handleDelay;

        // stop displaying animal on UI
        UIMngr.GetComponent<UI_Manager>().stopDisplayingAnimal();
        
    }

    private void pickUpAnimal( GameObject animal )
    {
        // if animal was successfully picked up
        if ( animal.GetComponent<animal>().pickUp() )
        {
            isHoldingAnimal = true;
            animalHolding = animal;
            animalToPickUp = null;

            // display what animal were holding
            if ( animal.GetComponent<animal>().powerUpName == "speed")
            {
                UIMngr.GetComponent<UI_Manager>().showBunny();
            }

            if (animal.GetComponent<animal>().powerUpName == "jump")
            {
                UIMngr.GetComponent<UI_Manager>().showSheep();
            }


            // reset timer for handling animal
            timeToHandle = handleDelay;
        }

        else
        {
            Debug.Log("Could not pick up Animal! :( ");
        }
    }

    private void sacrifice( GameObject animal )
    {
        //Debug.Log("Sacrificing Animal! >:O ");

        // player is not holding animal anymore
        isHoldingAnimal = false;
        animalHolding = null;

        // reset timer for handling animal
        timeToHandle = handleDelay;

        UIMngr.GetComponent<UI_Manager>().stopDisplayingAnimal(); // were not holding animal anymore
        
        animal.GetComponent<animal>().sacrifice();
        Demon.GetComponent<demon>().animalToSacrifice = animal;
    }

    public void addPowerUp( string powerName, float value )
    {
        if ( powerName.ToLower() == "speed" && value <= speedLimit && value > -speedLimit )
        {
            hasPowerup = true;
            speedPowerup = value;
        }

        if (powerName.ToLower() == "jump" && value <= jumpLimit && value > -jumpLimit )
        {
            hasPowerup = true;
            jumpPowerup = value;
        }
    }

    public void removePowerUp()
    {
        hasPowerup = false;
        resetPowerUps();

    }

    private void resetPowerUps()
    {
        speedPowerup = 1;
        jumpPowerup = 1;
    }

    private void die()
    {
        // trigger death animation / sprite

        // after x seconds then tell scene manager to reload scene
        SceneMngr.GetComponent<LevelSwitch>().resetScene();

    }

}
