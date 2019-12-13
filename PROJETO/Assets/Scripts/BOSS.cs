using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOSS : MonoBehaviour {

    public Rigidbody2D bullet;
    public Transform[] shotSpawners;
    public float mimYForce, maxYForce;
    public float fireRateMin, fireRateMax;
    public GameObject Laser;
    public Transform LaserSpawn;
    public float minLaserTime, maxLaserTime;
    public int health;
    public int highscore;


    private bool isDead = false;
    private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        //sprite = GetComponent<SpriteRenderer>();
        ActivateBoss();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ActivateBoss()
    {
        //Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
        Debug.Log("01");
        Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
        Debug.Log("02");


    }

    void FireLaser()
    {
        Debug.Log("03");
        if (!isDead)
        {
            Debug.Log("04");
            Instantiate(Laser, LaserSpawn.position, LaserSpawn.rotation);
            Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
            Debug.Log("05");
        }
    }

    void Fire()
    {
        if (!isDead)
        {
            Rigidbody2D tempBullet = Instantiate(bullet, shotSpawners[Random.Range(0, shotSpawners.Length)].position, Quaternion.identity);
            tempBullet.AddForce(new Vector2(0, Random.Range(mimYForce, maxYForce)), ForceMode2D.Impulse);
            Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
        }
    }
    public void TookDamege(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
            Bullet[] bullets = FindObjectsOfType<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                bullet.gameObject.SetActive(false);
            }

            Invoke("LoadScene", 2f);

            GameManager.gManager.Score += highscore;
            FindObjectOfType<UIManager>().UpdateScore();

        }
        else
        {
            StartCoroutine(TookerDamageCoRoutine());
        }
    }
    IEnumerator TookerDamageCoRoutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;

    }

    void LoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
