using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float dir = 1f;
    public bool dirRight = true;
    private float timer;
    public float speed = 10f;
    private Rigidbody2D rb;
    public float lifetime = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {

        if (dirRight)
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
        rb.velocity = new Vector2(dir * speed * Time.fixedDeltaTime, 0);

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Player" && coll.tag != "Projectile" && coll.tag != "Environment")
        {
            gameObject.SetActive(false);
        }
    }


}
