using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemies : Singleton<SpawningEnemies>
{
    public float EnemyDeathCount;
    ObjectPooler objectPooler;
    GameObject player;

    public GameObject[] EnemyList;
    static float wantedLevel;
    [Header("Spawn Border")]
    public float xWidthMax;
    public float xWidthMin;
    public float zWidthMin;
    public float zWidthMax;

    Vector3 originPoint = Vector3.zero;

    [Header("1 Star Stats")]
    public float enemyCount1 = 5;
    public Queue<GameObject> AEnemyCount1 = new Queue<GameObject>();

    [Header("2 Star Stats")]
    public float enemyCount2 = 10;
    public Queue<GameObject> AEnemyCount2 = new Queue<GameObject>();

    [Header("3 Star Stats")]
    public float enemyCount3 = 15;
    public Queue<GameObject> AEnemyCount3 = new Queue<GameObject>();

    

    public void Start()
    {
        player = GameObject.Find("Player");
        EnemyDeathCount = 0;
    }
    void Update()
    {
        switch (PlayerController.Instance.score)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                PlayerController.Instance.playerWantedLevel = 0;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                PlayerController.Instance.playerWantedLevel = 1;
                break;
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                PlayerController.Instance.playerWantedLevel = 2;
                break;
            case 15:
                PlayerController.Instance.playerWantedLevel = 3;
                break;
            default:
                break;
        }

        wantedLevel = PlayerController.Instance.playerWantedLevel;

        if (wantedLevel == 1)
        {
            Spawn1StarWave();
        }
        else if (wantedLevel == 2)
        {
            Spawn2StarWave();
        }
        else if (wantedLevel == 3)
        {
            Spawn3StarWave();
        }
        else
        {
            return;
        }
    }
    void Spawn1StarWave()
    {
        while (AEnemyCount1.Count < enemyCount1)
        {

            RaycastHit hit;
            do
            {
                Vector3 currentPos = player.transform.position;
                Vector3 spawnPoint = currentPos;
                float transformX = Random.Range(xWidthMin, xWidthMax);
                float transformZ = Random.Range(xWidthMin, xWidthMax);
                spawnPoint.x += transformX;
                spawnPoint.z += transformZ;
                spawnPoint.y += 100;
                Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity);
            }
            while (hit.collider == null || !hit.collider.CompareTag("Ground"));

            GameObject enemy = (GameObject)Instantiate(EnemyList[0], hit.point, Quaternion.identity);
            AEnemyCount1.Enqueue(enemy);
            
        }
    }
    void Spawn2StarWave()
    {
        while (AEnemyCount2.Count < enemyCount2)
        {
            RaycastHit hit;
            do
            {
                Vector3 currentPos = player.transform.position;
                Vector3 spawnPoint = currentPos;
                float transformX = Random.Range(xWidthMin, xWidthMax);
                float transformZ = Random.Range(xWidthMin, xWidthMax);
                spawnPoint.x += transformX;
                spawnPoint.z += transformZ;
                spawnPoint.y += 100;
                Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity);
            }
            while (hit.collider == null || !hit.collider.CompareTag("Ground"));

            GameObject enemy = (GameObject)Instantiate(EnemyList[1], hit.point, Quaternion.identity);
            AEnemyCount2.Enqueue(enemy);
        }

   
    }
    void Spawn3StarWave()
    {
        while (AEnemyCount3.Count < enemyCount3)
        {
            RaycastHit hit;
            do
            {
                Vector3 currentPos = player.transform.position;
                Vector3 spawnPoint = currentPos;
                float transformX = Random.Range(xWidthMin, xWidthMax);
                float transformZ = Random.Range(xWidthMin, xWidthMax);
                spawnPoint.x += transformX;
                spawnPoint.z += transformZ;
                spawnPoint.y += 100;
                Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity);
            }
            while (hit.collider == null || !hit.collider.CompareTag("Ground"));

            GameObject enemy = (GameObject)Instantiate(EnemyList[2], hit.point, Quaternion.identity);
            AEnemyCount3.Enqueue(enemy);
        }
    }

    public void RemoveFromList(string name)
    {

        switch (name)
        {
            case "Police01(Clone)":
                AEnemyCount1.Dequeue();
                EnemyDeathCount++;
                break;
            case "Police02(Clone)":
                AEnemyCount2.Dequeue();
                EnemyDeathCount++;
                break;
            case "Police03(Clone)":
                AEnemyCount3.Dequeue();
                EnemyDeathCount++;
                break;

            default:
                Debug.Log("Not In list to Remove, name was: " + name);
                break;
        }
    }

    public void ClearQueuess()
    {
        if (AEnemyCount1.Count > 0)
        {
            AEnemyCount1.Clear();
        }
        if (AEnemyCount2.Count > 0)
        {
            AEnemyCount2.Clear();
        }
        if (AEnemyCount3.Count > 0)
        {
            AEnemyCount3.Clear();
        }
    }
}
