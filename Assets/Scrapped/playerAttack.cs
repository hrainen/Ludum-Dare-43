using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private float timeTillAttack = 0.0f;
    public float timeBetweenAttack = 0.3f;

    public Transform attackPosition;
    public float attackRadius;
    public float attackLen;
    public float attackWidth;
    public LayerMask enemyMask;
    public float Damage = 25;


	// Update is called once per frame
	void Update () {
        if ( timeTillAttack <= 0 && Input.GetMouseButtonDown( 0 ) ){
            timeTillAttack = timeBetweenAttack; // resets the attack rate

            attack();
        }
        else
        {
            if (timeTillAttack > 0)
            {
                timeTillAttack -= Time.deltaTime;
            }
        }
	}

    private void attack()
    {
        Collider2D[] enmiesToDamage = Physics2D.OverlapBoxAll(attackPosition.position, new Vector2( attackLen, attackWidth ) , 0, enemyMask);
        for (int i = 0; i < enmiesToDamage.Length; i++)
        {
            enmiesToDamage[i].GetComponent<enemy>().takeDamage( Damage );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( attackPosition.position, new Vector3 ( attackLen, attackWidth, 0 ));
    }
}
