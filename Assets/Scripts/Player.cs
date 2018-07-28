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

    [SerializeField]
    private GameObject[] engines;
    public bool shieldActive = false;
    public bool canTripleShot = false;
    public bool canSpeed = false;


    private SpawnManager SM;
    private UIManager UIM;
    private GameManager GM;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, 0);
        UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (UIM)
        {
            UIM.UpdateLives(_lives);
        }
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (SM)
        {
            SM.StartSpawn();
        }
        audioSource = GetComponent<AudioSource>();
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
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
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
            audioSource.Play();
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
            if (_lives == 2)
            {
                engines[Random.Range(0, 2)].SetActive(true);
            }
            else if (_lives == 1)
            {
                foreach (var x in engines)
                {
                    x.SetActive(true);
                }
            }
            if (UIM)
            {
                UIM.UpdateLives(_lives);
            }
            if (_lives == 0)
            {
                GM.gameOver = true;
                UIM.ShowTitleScreen();
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
