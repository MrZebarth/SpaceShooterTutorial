﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private GameObject laserPrefab;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Shoot();
        
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float velocity = Time.deltaTime * _speed;
        transform.Translate(velocity * horizontalInput, velocity * verticalInput, 0);
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.3f)
        {
            transform.position = new Vector3(transform.position.x, -3.3f, 0);
        }
        if (transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && _nextFire < Time.time)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(laserPrefab,transform.position+new Vector3(0,0.88f,0),Quaternion.identity);
        }
    }
}
