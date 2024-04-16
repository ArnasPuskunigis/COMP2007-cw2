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
    public bool gameStarted;

    public TextMeshProUGUI enemiesLeft;

    public int spawnerIndex;

    public winnerUi winningUi;

    public int currentWaveNumber;

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

    public void startBattle()
    {
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesList.Count == 0 && !isSpawning && gameStarted)
        {
            isSpawning = true;
            newWave();
        }
    }

    public void spawnNewWave(int waveEnemyCount)
    {
        for (int i = 0; i < waveEnemyCount; i++)
        {
            float temp = Random.Range(5f, 10f);
            Invoke("spawnEnemy", temp);
        }
    }

    public void spawnEnemy()
    {
        spawnerIndex = Random.Range(0, spawnPoints.Length - 1);
        enemiesList.Add(Instantiate(enemyPref, spawnPoints[spawnerIndex].position, spawnPoints[spawnerIndex].rotation, enemyParent.transform));
        isSpawning = false;
        enemiesLeft.text = currentWaveNumber.ToString();
    }

    public void newWave()
    {
        if (currentWave == 0)
        {
            currentWave++;
            currentWaveNumber = wave1EnemyCount;
            spawnNewWave(wave1EnemyCount);
        }
        else if (currentWave == 1)
        {
            currentWave++;
            currentWaveNumber = wave2EnemyCount;
            spawnNewWave(wave2EnemyCount);
        }
        else if (currentWave == 2)
        {
            currentWave++;
            currentWaveNumber = wave3EnemyCount;
            spawnNewWave(wave3EnemyCount);
        }
        else if (currentWave == 3)
        {
            currentWave++;
            currentWaveNumber = wave4EnemyCount;
            spawnNewWave(wave4EnemyCount);
        }
        else if(currentWave == 4)
        {
            winGame();
        }
    }

    public void winGame()
    {
        winningUi.showPanel();
        enemiesLeft.text = "0";
    }

}
