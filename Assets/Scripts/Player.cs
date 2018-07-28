using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject shieldGameObject;
    public bool shieldActive = false;
    public bool canTripleShot = false;
    public bool canSpeed = false;

    private UIManager UIM;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, 0);
        UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (UIM)
        {
            UIM.UpdateLives(_lives);
        }
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
        float boost = (canSpeed ? 2f : 1f);
        float velocity = Time.deltaTime * _speed * boost;
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
            if (canTripleShot)
            {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
                
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
        }
    }

    public void TripleShot()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
        
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        canTripleShot = false;
    }

    public void SpeedBoost()
    {
        canSpeed = true;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5f);
        canSpeed = false;
    }

    public void Damage()
    {
        if (shieldActive)
        {
            shieldActive = false;
            shieldGameObject.SetActive(false);
            return;
        }
        else
        {
            _lives--;
            if (UIM)
            {
                UIM.UpdateLives(_lives);
            }
            if (_lives == 0)
            {
                Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    public void Shield()
    {
        shieldActive = true;
        shieldGameObject.SetActive(true);
    }
}
