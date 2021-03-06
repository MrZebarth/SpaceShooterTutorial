﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    float speed = 3.0f;
    [SerializeField]
    private int powerupID; //0=triple shot, 1=speed boost, 2=shield
    [SerializeField]
    private AudioClip clip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            if (p)
            {
                if (powerupID == 0)
                {
                    p.TripleShot();
                }
                else if (powerupID == 1)
                {
                    p.SpeedBoost();
                }
                else if (powerupID == 2)
                {
                    p.Shield();
                }
                
            }
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
