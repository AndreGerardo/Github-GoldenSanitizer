using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteManager : MonoBehaviour
{

    private float length, startPos;
    public GameObject cam;
    public float parallaxThreshold;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxThreshold);
        float distance = cam.transform.position.x * parallaxThreshold;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
