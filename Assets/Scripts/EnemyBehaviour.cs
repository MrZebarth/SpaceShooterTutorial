using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private GameObject explosion;
    private UIManager UIM;
    private GameManager GM;
    [SerializeField]
    private AudioClip clip;

	// Use this for initialization
	void Start () {
        UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        //when off the screen
        if (transform.position.y < -5.25f)
        {
            //respawn with new x if not game over
            if (!GM.gameOver)
            {
                float randomX = Random.Range(-8f, 8f);
                transform.position = (new Vector3(randomX, 6f, 0));
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //destroy the other
            Destroy(other.gameObject);
            //destroy yourself
            if (UIM)
            {
                UIM.UpdateScore();
            }
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
