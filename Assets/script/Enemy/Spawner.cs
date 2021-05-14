using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoSingleton<Spawner>
{

    public List<GameObject> enemyListOne, enemyListTwo;
    public GameObject enemyPrefabOne, enemyPrefabTwo;
    public bool canIncreaseEnemy = true;
    int enemyCount = 5;

    public Transform[] spawnPos;
    public float spawnRate = 0.75f;
    public float MaxTime = 5f;
    float timer = 0f;

    void Start()
    {
        enemyListOne = new List<GameObject>();
        enemyListTwo = new List<GameObject>();

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj1 = (GameObject)Instantiate(enemyPrefabOne);
            GameObject obj2 = (GameObject)Instantiate(enemyPrefabTwo);
            obj1.SetActive(false);
            obj2.SetActive(false);
            enemyListOne.Add(obj1);
            enemyListTwo.Add(obj2);
        }
    }

    void Update()
    {
        if (timer < MaxTime)
        {
            timer += Time.deltaTime;
        }

        if (timer >= spawnRate)
        {
            int randomizer = (int)Random.Range(0, 2);

            if (randomizer == 0)
            {
                GameObject obj = (GameObject)SpawnEnemyOne();

                if (obj == null)
                {
                    return;
                }

                obj.transform.position = spawnPos[Random.Range(0, 2)].position + (Vector3.up * Random.Range(-2.5f, 3f));
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
            }
            else
            {
                GameObject obj = (GameObject)SpawnEnemyTwo();

                if (obj == null)
                {
                    return;
                }

                obj.transform.position = spawnPos[Random.Range(0, 2)].position + (Vector3.up * Random.Range(-2.5f, 3f));
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
            }
            timer = 0f;
        }

    }

    public GameObject SpawnEnemyOne()
    {
        for (int i = 0; i < enemyListOne.Count; i++)
        {
            if (enemyListOne[i].activeInHierarchy == false)
            {
                return enemyListOne[i];
            }
        }

        if (canIncreaseEnemy)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefabOne);
            enemyListOne.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject SpawnEnemyTwo()
    {
        for (int i = 0; i < enemyListTwo.Count; i++)
        {
            if (enemyListTwo[i].activeInHierarchy == false)
            {
                return enemyListTwo[i];
            }
        }

        if (canIncreaseEnemy)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefabTwo);
            enemyListTwo.Add(obj);
            return obj;
        }

        return null;
    }

}
