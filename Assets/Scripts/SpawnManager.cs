using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps;
    private GameManager GM;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
	}
	
    public void StartSpawn()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
    }
	IEnumerator EnemySpawn() 
    {
        while (!GM.gameOver)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8f, 8f), 8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpSpawn()
    {
        while (!GM.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerup], new Vector3(Random.Range(-8f, 8f), 8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }
    }

}
