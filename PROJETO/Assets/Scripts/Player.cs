using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public float jumpForce = 1000;
    public GameObject bulletPrefab;
    public Transform shotSpawner;
    public Rigidbody2D bomb;
    public float damageTime = 1f;
    public bool canFire = true;

    private Animator anim;
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hForce = 0;
    private bool crouched;
    private bool lookingUp;
    private bool realoading;
    private float fireRate = 0.5f;
    private float nextFire;
    private bool tookDamage = false;

    private int bullets;
    private float realoadTime;
    private int health;
    private int maxHealth;
    private int bombs;
    

    private bool isDead = false;

    GameManager gManager;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();

        gManager = GameManager.gManager;

        SetPlayerStatus();
        bombs = gManager.bombs;
        health = maxHealth;

        UpdateBulletUI();
        UpdateBombUI();
        UpdateHealthUI();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //verifica se não esta morto
        if (!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            //quando estiver no chão define a animação jump falsa
            if (onGround)
            {
                anim.SetBool("Jump", false);
            }

            if (Input.GetButtonDown("Jump") && onGround && !realoading)
            {
                jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (rb2d.velocity.y > 0)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                }
            }

            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && bullets > 0 && !realoading && canFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
                if (!facingRight && !lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if(!facingRight && lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                if(crouched && !onGround)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, -90);
                }

                bullets--;
                UpdateBulletUI();
            }
            else if(Input.GetButtonDown("Fire1") && bullets <=0 && onGround)
            {
                StartCoroutine(Realoading());
            }

            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");

            anim.SetBool("lookingUp", lookingUp);
            anim.SetBool("Crouched", crouched);

            if (Input.GetButtonDown("Reaload") && onGround)
            {
                StartCoroutine(Realoading());
            }

            if(Input.GetButtonDown("Fire2")&& bombs > 0)
            {
                Rigidbody2D tempBomb = Instantiate(bomb, transform.position, transform.rotation);
                if (facingRight)
                {
                    tempBomb.AddForce(new Vector2(8, 10), ForceMode2D.Impulse);
                }
                else
                {
                    tempBomb.AddForce(new Vector2(-8, 10), ForceMode2D.Impulse);
                }

                bombs--;
                UpdateBombUI();
            }

            if((crouched || lookingUp || realoading) && onGround)
            {
                hForce = 0;
            }
        }
	}

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if(!crouched && !lookingUp && !realoading)
                hForce = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(hForce));

            rb2d.velocity = new Vector2(hForce * speed, rb2d.velocity.y);

            if(hForce > 0 && !facingRight)
            {
                Flip();
            }
            else if(hForce < 0 && facingRight)
            {
                Flip();
            }

            if (jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb2d.AddForce(Vector2.up * jumpForce);
            }
        }
    
    }

    IEnumerator Realoading()
    {
        realoading = true;
        anim.SetBool("Realoading", true);
        yield return new WaitForSeconds(realoadTime);
        bullets = gManager.bullets;
        realoading = false;
        anim.SetBool("Realoading", false);
        UpdateBulletUI();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
        
    public void SetPlayerStatus()
    {
        fireRate = gManager.fireRate;
        bullets = gManager.bullets;
        realoadTime = gManager.realoadTime;
        maxHealth = gManager.health;

    }

    void UpdateBulletUI()
    {
        FindObjectOfType<UIManager>().UpdateBulletUI(bullets);
    }
    
    void UpdateBombUI()
    {
        FindObjectOfType<UIManager>().UpdateBombs(bombs);
        gManager.bombs = bombs;
    }

    void UpdateHealthUI()
    {
        FindObjectOfType<UIManager>().UpdateHealthUI(health);
    }

    void UpdateCoinUI()
    {
        FindObjectOfType<UIManager>().UpdateCoin();
    }

    void UpdateScoreUI()
    {
        FindObjectOfType<UIManager>().UpdateScore();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")&& !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && tookDamage)
        {
            StartCoroutine(TookDamage());
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            gManager.coins += 1;
            UpdateCoinUI();

        }


    }

    IEnumerator TookDamage()
    {
       
        tookDamage = true;
        health--;
        UpdateHealthUI();
        
        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            //Invoke("RealoadScene", 2f);
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9, 10);
            for (float i = 0; i <damageTime; i+= 0.2f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);

            }

            Physics2D.IgnoreLayerCollision(9, 10, false);
            tookDamage = false;
        }
    }

    //void RealoadScene()
   // {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   // }

    public void SetHealthAndBombs(int life, int bomb)
    {
        health += life;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        bombs += bomb;
        UpdateBombUI();
        UpdateHealthUI();

    }
}