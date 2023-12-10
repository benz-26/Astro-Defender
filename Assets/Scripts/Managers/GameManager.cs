using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject tutorPanel;
    [SerializeField] private GameObject[] towers;

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1f;

        // Initialize game over panels
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }
    }

    private void Start()
    {
        DisablePlayer();
        tutorPanel.SetActive(true);
    }

    public void EnablePlayer()
    {
        playerManager.enabled = true;
        tutorPanel.SetActive(false);
        for (int i = 0; i < towers.Length; i++)
        {
            Transform spawnChild = towers[i].transform.Find("ESpawn");
            if (spawnChild != null)
            {
                EnemyTowerSpawner spawner = spawnChild.GetComponent<EnemyTowerSpawner>();
                if (spawner != null)
                {
                    spawner.enabled = true;
                    Debug.Log("Enabled spawner in tower " + i);
                }
            }
        }
    }

    private void DisablePlayer()
    {
        playerManager.enabled = false;
        for (int i = 0; i < towers.Length; i++)
        {
            Transform spawnChild = towers[i].transform.Find("ESpawn");
            if (spawnChild != null)
            {
                EnemyTowerSpawner spawner = spawnChild.GetComponent<EnemyTowerSpawner>();
                if (spawner != null)
                {
                    spawner.enabled = false;
                    Debug.Log("Disabled spawner in tower " + i);
                }
            }
        }
    }

}
