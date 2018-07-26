using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        //when off the screen
        if (transform.position.y < -5.25f)
        {
            //respawn with new x
            float randomX = Random.Range(-8f, 8f);
            transform.position = (new Vector3(randomX, 6f, 0));
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //destroy the other
            Destroy(other.gameObject);
            //destroy yourself

            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
