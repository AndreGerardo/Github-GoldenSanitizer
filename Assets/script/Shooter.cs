using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoSingleton<Shooter>
{

    public List<GameObject> projectileList;
    public GameObject projectilePrefab;
    public bool canIncreaseProjectile = true;
    int projectileCount = 10;

    void Start()
    {
        projectileList = new List<GameObject>();

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectileList.Add(obj);
        }
    }

    public GameObject SpawnBullet()
    {
        for (int i = 0; i < projectileList.Count; i++)
        {
            if (projectileList[i].activeInHierarchy == false)
            {
                return projectileList[i];
            }
        }

        if (canIncreaseProjectile)
        {
            GameObject obj = (GameObject)Instantiate(projectilePrefab);
            projectileList.Add(obj);
            return obj;
        }

        return null;
    }

}
