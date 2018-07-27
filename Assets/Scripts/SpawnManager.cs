using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps;


	// Use this for initialization
	void Start () {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
	}
	
	IEnumerator EnemySpawn() 
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8f, 8f), 8f, 0), Quaternion.identity);
        }
    }

    IEnumerator PowerUpSpawn()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3);
            yield return new WaitForSeconds(8.0f);
            Instantiate(powerUps[randomPowerup], new Vector3(Random.Range(-8f, 8f), 8f, 0), Quaternion.identity);
        }
    }

}
