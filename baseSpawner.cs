using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseSpawner : MonoBehaviour
{
    public static baseSpawner instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Fehler!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public Transform enemyprefab;
    public Transform footenemyprefab;

    public Transform spawnpoint;

    public float enemyperwave = 3f;
    public float enemyspawned = 0f;
    public float wavecount = 1f;
    public float countdown = 5f;
    public float totalwaves = 0f;

    public int randfootmax=5;
    public int randfootmin=1;
    public int randhorsemax=2;
    public int randhorsemin=0;

    public int horesesspawned = 0;
    public int footspawned = 0;

    public int randfoot;
    public int randhorse;

    public int specificenemies;

    void Start()
    {
        calculateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        spawnBehaviour();
    }

    void spawnBehaviour()
    {
        if (wavecount == 3)      //jede 3te Welle mehr gegner
        {
            randfootmin++;
            randfootmax += 2;
            randhorsemin++;
            randhorsemax++;

            wavecount = 0f;
        }
        
        if (countdown <= 0)
        {
            countdown = 4f;
            if (horesesspawned < randhorse)
            {
                spawnEnemy(1);
                horesesspawned++;
            }
            if (footspawned < randfoot)
            {
                spawnEnemy(2);
                footspawned++;
            }


            enemyspawned++;
        }
        countdown -= Time.deltaTime;
        if (enemyspawned == specificenemies)
        {
            wavecount++;
            totalwaves++;
            countdown = 8f;
            enemyspawned = 0f;

            horesesspawned = 0;
            footspawned = 0;

            calculateEnemies();
        }
    }

    void calculateEnemies()
    {
        randfoot = Random.Range(randfootmin, randfootmax);
        randhorse = Random.Range(randhorsemin, randhorsemax);

        specificenemies = randfoot + randhorse;
    }

    void spawnEnemy(int whattospawn)
    {
        Vector3 footenemyspawnpoint = spawnpoint.position;
        footenemyspawnpoint.y += 2;

        switch (whattospawn)
        {
            case (1): Instantiate(enemyprefab, spawnpoint.position, spawnpoint.rotation);break;
            case (2): Instantiate(footenemyprefab, footenemyspawnpoint, spawnpoint.rotation);break;
        }
    }
}
