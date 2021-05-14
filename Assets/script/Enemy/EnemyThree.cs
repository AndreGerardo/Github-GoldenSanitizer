using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : MonoBehaviour
{
    public float dir = 1f;
    public bool dirRight = true;
    private float timer;
    public float speed = 10f;
    private Rigidbody2D rb;
    public float lifetime = 5f;
    private AudioSource audio;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {

        if (transform.position.x < 0)
        {
            dir = 1f;
            Vector3 newDir = transform.localScale;
            newDir.x = 1f;
            transform.localScale = newDir;
        }
        else
        {
            dir = -1f;
            Vector3 newDir = transform.localScale;
            newDir.x = -1f;
            transform.localScale = newDir;
        }

        timer = 0;
    }

    private void Update()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(dir * speed * Time.fixedDeltaTime, 0);

            timer += Time.deltaTime;

            if (timer >= lifetime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Environment" && coll.tag != "Enemy")
        {
            audio.Play();
            GetComponent<CircleCollider2D>().isTrigger = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = rb.velocity * 0f;
            isDead = true;
            
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            audio.Play();
            GetComponent<CircleCollider2D>().isTrigger = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isDead = false;

            gameObject.SetActive(false);
        }
    }
}
