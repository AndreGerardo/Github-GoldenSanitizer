using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float rotateSpeed = 200f;
    private Rigidbody2D rb;
    private AudioSource audio;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed * Time.fixedDeltaTime;
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
        if(coll.collider.tag == "Player")
        {
            audio.Play();
            GetComponent<CircleCollider2D>().isTrigger = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isDead = false;
            gameObject.SetActive(false);
        }
    }
}
