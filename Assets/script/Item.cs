using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer sprite;
    public float spawnTimer = 5f;
    public AudioSource audio;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && sprite.enabled)
        {
            audio.Play();
            StartCoroutine(PickedUp());
        }
    }

    IEnumerator PickedUp()
    {
        sprite.enabled = false;

        yield return new WaitForSeconds(spawnTimer);

        sprite.enabled = true;

    }
}
