using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoSingleton<Player>
{
    //GameManager
    public GameManager gm;

    [Header("Movement and Jump")]
    //Movement & jump
    public  bool        CONTROLDEBUG = true;
    public  Joystick    joycon;
    public  Rigidbody2D rb;
    public  float       speed = 10f;
    public  float       jumpforce = 1000f;
    public  float       dir;
    public  bool        isFacingRight = true;
    public  bool        isGrounded = true;
    public  LayerMask   whatIsGround;
    private float       checkRadius = 0.05f;
    public  Transform   groundCheck;
    public  int         extraJump = 1;
    public  Vector3     spawnPos;

    [Header("Shooting")]
    //Shooting
    public Transform gunPos;
    public float fireRate = 0.25f;
    public float timer = 0f;
    public int ammo = 7;
    int prevAmmo;
    public bool shots = false;
    public Slider ammoBar;

    [Header("Item")]
    //Item
    public bool vulnerable = true;
    public SpriteRenderer glowSprite;
    public float invulnerableTime = 5f;

    [Header("Cure")]
    //Enemy
    public bool isCured = false;
    public bool isCuring = false;
    public float cureChance = 0.95f;
    public float cureChanceInc = 0.00075f;
    public float cureTime = 4f;
    private float cureTimer;
    public GameObject cureSprite;

    [Header("Animation")]
    //Animation
    public Animator anim;
    public GameObject deathPanel;

    [Header("Audio")]
    //Audio
    public AudioSource jumpSound;
    public AudioSource shootSound;
    public AudioSource deathSound;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawnPos = transform.position;
    }

    public void Update()
    {
        //Direction manager
        if (CONTROLDEBUG)
        {
            dir = Input.GetAxisRaw("Horizontal");
        }else
        {
            if (joycon.Horizontal > 0.2f)
            {
                dir = 1;
            } else if (joycon.Horizontal < -0.2f)
            {
                dir = -1;
            }else
            {
                dir = 0;
            }
        }

        if (dir > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 newDir = transform.localScale;
            newDir.x *= -1f;
            transform.localScale = newDir;

        } else if (dir < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 newDir = transform.localScale;
            newDir.x *= -1f;
            transform.localScale = newDir;
        }

        //Movement & jump
        rb.velocity = new Vector2(dir * Time.fixedDeltaTime * speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            extraJump--;
            rb.velocity = Vector2.up * jumpforce * Time.fixedDeltaTime;
            jumpSound.Play();
        }

        if (isGrounded) extraJump = 1;

        //Shooting
        timer += Time.deltaTime;
        
        if ((Input.GetKey(KeyCode.L) || shots ) && timer >= fireRate && ammo > 0f)
        {
            Shoot();
            anim.SetTrigger("Shoot");
            timer = 0;
            ammo--;
            ammoBar.value = ammo;
            prevAmmo = ammo;
        }

        if (prevAmmo != ammo)
        {
            ammoBar.value = ammo;
            prevAmmo = ammo;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo = 5;
            ammoBar.value = ammo;
        }
        
        //Animation
        anim.SetFloat("MovementVel", Mathf.Abs(dir));
        anim.SetFloat("VerticalVel", rb.velocity.y);
        anim.SetBool("Grounded", isGrounded);

        //Death
        if (transform.position.y < -5 && vulnerable)
        {
            GameOver();
        }

        //Curing
        if(isCuring)
        { 
            cureTimer += Time.fixedDeltaTime;
            if (cureTimer > cureTime)
            {
                if(Random.value > cureChance)
                {
                    isCured = true;
                    isCuring = false;
                    cureSprite.SetActive(false);
                    GameOver();
                }else
                {
                    cureChance -= cureChanceInc;
                    cureSprite.SetActive(false);
                    isCuring = false;
                }
            }

        }else
        {
            cureTimer = 0f;
        }
    }

    public void GameOver()
    {
        transform.position = spawnPos + (Vector3.up * 5);
        gm.isPlaying = false;
        deathSound.Play();
        deathPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item"))
        {
            StartCoroutine(PakeMasker());
        }

        if (coll.CompareTag("Enemy") && vulnerable)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy"))
        {
            if(!isCuring)
            {
                isCuring = true;
                cureSprite.SetActive(true);
            }
        }
    }

    IEnumerator PakeMasker()
    {
        vulnerable = false;
        glowSprite.enabled = true;
        yield return new WaitForSeconds(invulnerableTime);
        glowSprite.enabled = false;
        vulnerable = true;
    }

    public void Jump()
    {
        if (extraJump > 0)
        {
            extraJump--;
            rb.velocity = Vector2.up * jumpforce * Time.fixedDeltaTime;
            jumpSound.Play();
        }
    }

    public void ShootButton(bool shot)
    {
        shots = shot;
    }

    public void Shoot()
    {
        GameObject obj = GetComponent<Shooter>().SpawnBullet();

        if (obj == null)
        {
            return;
        }

        shootSound.Play();

        obj.GetComponent<Projectile>().dirRight = isFacingRight;
        obj.transform.position = gunPos.position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
    }

}
