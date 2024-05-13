using UnityEngine;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField]
    GameObject PrefabToInstantiate;
    public TextMeshProUGUI timerGUI;

    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool readyToCountDown;
    private void Start()
    {
        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            Debug.Log("AHHHHH");
            waves[i].enemiesLeft = waves[i].numEnemies;
        }
    }
    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("You survived every wave!");
            timerGUI.text = "You Win!";
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerdata>().EndLevel();
        }

        if (readyToCountDown)
        {
            timerGUI.text = "Time To Next Wave: " + countdown.ToString();
            countdown -= Time.deltaTime;
            Debug.Log(countdown);
        }

        if (countdown <= 0)
        {
            // Debug.Log("COUNTDOWN");
            timerGUI.text = "Time To Next Wave: " + 0;
            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            StartCoroutine(SpawnWave());
            Debug.Log("COUNTDOWN");
        }

        if(currentWaveIndex < waves.Length)
        {
            if (waves[currentWaveIndex].enemiesLeft == 0) // Add the condition here
            {
                currentWaveIndex++;
                if (currentWaveIndex < waves.Length)
                {
                    readyToCountDown = true;
                }
            }
        }
    }
    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
           Debug.Log("wave index"+ currentWaveIndex);
            for (int i = 0; i < waves[currentWaveIndex].numEnemies; i++)
            {
                Debug.Log("WAVVVESSS");
                CreateObject(spawnPoint.transform.position);
                // Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
                //waves[currentWaveIndex].enemiesLeft--;
                // enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    public void CreateObject(Vector3 position)
    {
        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }
        GameObject obj = Instantiate(
        PrefabToInstantiate,
        position,
        Quaternion.identity);
        Debug.Log(obj.transform.position);
    }
}

[System.Serializable]
public class Wave
{
    public int numEnemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}