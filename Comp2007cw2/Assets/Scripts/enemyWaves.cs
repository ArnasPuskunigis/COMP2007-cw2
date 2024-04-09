using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyWaves : MonoBehaviour
{

    public int wave1EnemyCount;
    public int wave2EnemyCount;
    public int wave3EnemyCount;
    public int wave4EnemyCount;

    public List<GameObject> enemiesList;
    public GameObject enemyPref;

    public Transform[] spawnPoints;
    public GameObject enemyParent;

    public int currentWave;

    public bool isSpawning;

    public TextMeshProUGUI enemiesLeft;


    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        enemiesLeft.text = "";
    }

    public void removeEnemy(GameObject enemy)
    {
        enemiesList.Remove(enemy);
        Destroy(enemy);
        enemiesLeft.text = enemiesList.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesList.Count == 0 && !isSpawning)
        {
            isSpawning = true;
            newWave();
        }
    }

    public void spawnNewWave(int waveEnemyCount)
    {
        for (int i = 0; i < waveEnemyCount; i++)
        {
            int temp = Random.Range(0, spawnPoints.Length - 1);
            enemiesList.Add(Instantiate(enemyPref, spawnPoints[temp].position, spawnPoints[temp].rotation, enemyParent.transform));
        }
        enemiesLeft.text = waveEnemyCount.ToString();
        isSpawning = false;
    }

    public void newWave()
    {
        if (currentWave == 0)
        {
            currentWave++;
            spawnNewWave(wave1EnemyCount);
        }
        else if (currentWave == 1)
        {
            currentWave++;
            spawnNewWave(wave2EnemyCount);
        }
        else if (currentWave == 2)
        {
            currentWave++;
            spawnNewWave(wave3EnemyCount);
        }
        else if (currentWave == 3)
        {
            currentWave++;
            spawnNewWave(wave4EnemyCount);
        }
        else
        {
            winGame();
        }
    }

    public void winGame()
    {
        enemiesLeft.text = "0";
        print("winner");
    }


}
