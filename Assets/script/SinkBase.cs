using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBase : MonoBehaviour
{
    public Transform target;
    public Player player;
    public float rangeX, rangeY;
    public float reloadTime = 1f;
    public SpriteRenderer sinkGlowSprite;
    private float timer;
    public AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (Mathf.Abs(transform.position.x - target.position.x) < rangeX && Mathf.Abs(transform.position.y - target.position.y) < rangeY && player.ammo < 5)
        {
            if (timer < 2f)
                timer += Time.deltaTime;

            sinkGlowSprite.enabled = true;

            if (timer > reloadTime)
            {
                audio.Play();
                player.ammo++;
                timer = 0f;
            }
        }
        else
        {
            sinkGlowSprite.enabled = false;
        }
    }


}
