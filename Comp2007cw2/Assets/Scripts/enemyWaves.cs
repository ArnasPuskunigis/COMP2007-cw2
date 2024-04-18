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

    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the wave to 0 so no enemies spawn and the enemy text to nothing as there should be no enemies at the start
        currentWave = 0;
        enemiesLeft.text = "";
    }

    public void removeEnemy(GameObject enemy)
    {
        //Removes an enemy from the enemy list and then destroys the enemy object and updates enemy count
        enemiesList.Remove(enemy);
        Destroy(enemy);
        enemiesLeft.text = enemiesList.Count.ToString();
    }

    public void startBattle()
    {
        //This is used to start the enemy waves
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if to spawn enemies
        if(enemiesList.Count == 0 && !isSpawning && gameStarted)
        {
            isSpawning = true;
            newWave();
        }
    }

    public void spawnNewWave(int waveEnemyCount)
    {
        //Generate a time at which to spawn an enemy and then spawn that enemy after "temp" time has passed
        for (int i = 0; i < waveEnemyCount; i++)
        {
            float temp = Random.Range(5f, 10f);
            Invoke("spawnEnemy", temp);
        }
    }

    public void spawnEnemy()
    {
        //Spawn an enemy at a random spawnpoint from the list
        spawnerIndex = Random.Range(0, spawnPoints.Length - 1);
        enemiesList.Add(Instantiate(enemyPref, spawnPoints[spawnerIndex].position, spawnPoints[spawnerIndex].rotation, enemyParent.transform));
        //This is used to make sure that no enemies are spawned after the initial set of enemies has been spawned
        isSpawning = false;
        enemiesLeft.text = currentWaveNumber.ToString();
    }

    public void newWave()
    {
        //Checks the current wave and updates the wave spawning variables accordigly 
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
            winGame();
        }
        
    }

    public void winGame()
    {
        //Plays the winning sound and shows the wining ui
        winSound.Play();
        winningUi.showPanel();
        enemiesLeft.text = "0";
    }

}
