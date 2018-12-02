using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public int numEnemies = 3;
    private int enemiesLeft;
    public float timeBetweenEnemy = 2.5f;
    private float timeTilSpawn;

    public GameObject enemy; // enemy prefab that we are going to spawn

	// Use this for initialization
	void Start () {
        timeTilSpawn = timeBetweenEnemy;
        enemiesLeft = numEnemies;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeTilSpawn <= 0 && enemiesLeft > 0)
        {
            spawnEnemy();
        }

        else
        {
            timeTilSpawn -= Time.deltaTime;
        }

	}

    private void spawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        enemiesLeft--;
        timeTilSpawn = timeBetweenEnemy;
    }
}
