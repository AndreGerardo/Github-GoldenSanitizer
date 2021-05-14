using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform target;
    public float smoothThreshold = 0.125f;

    public float maxMap, minMap;

    public Vector3 offset;

    private void FixedUpdate()
    {

        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothThreshold * Time.fixedDeltaTime);
        if(target.position.x > minMap && target.position.x < maxMap)
            transform.position = new Vector3(smoothPos.x, transform.position.y, transform.position.z);

    }

}
