using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    // Damage to enemy
    public float damagePerSecond;
    public float damageInterval = 1f; // Interval at which damage is applied

    public float towerHealth;
    public float maxTowerHealth;

    public float damageFromEnemy; // Damage dealt by enemies to the tower
    public float damageIntervalFromEnemy = 1f; // Interval at which damage is applied by enemies to the tower

    private IEnumerator damageCoroutine;
    private IEnumerator damageFromEnemyCoroutine;
    private bool isTakingDamage = false;

    // Upgrade variables
    public int towerLevel = 1;
    public int towerUpgradeCost;
    public float towerUpgradeDamage;
    public float towerUpgradeRange;
    public float towerUpgradeInterval;
    public int towerUpgradeHealth;
    public int maxTowerLevel = 10;

    private List<EnemyTower> activeEnemies = new List<EnemyTower>();

    //inventory
    public ItemManager itemManager;

    public GameObject towerUpgradeButton;
    public GameObject[] towerUpgradeIndicator;

    private void Start()
    {
        towerUpgradeButton.SetActive(false); // Hide the tower upgrade button

        itemManager = GameObject.Find("Player").GetComponent<ItemManager>();
        if (itemManager == null)
        {
            Debug.LogError("ItemManager component not found on " + gameObject.name);
        }else
        {
            Debug.Log("Log");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyTower = collision.gameObject.GetComponent<EnemyTower>();
            if (enemyTower != null)
            {
                activeEnemies.Add(enemyTower);

                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
                damageCoroutine = ApplyDamageOverTime(enemyTower, damagePerSecond, damageInterval);
                StartCoroutine(damageCoroutine);

                if (damageFromEnemyCoroutine == null)
                {
                    damageFromEnemyCoroutine = ApplyDamageForTowerOverTime(damageFromEnemy, damageIntervalFromEnemy);
                    StartCoroutine(damageFromEnemyCoroutine);
                }
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            towerUpgradeButton.SetActive(true); // Display the tower upgrade button
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyTower = collision.gameObject.GetComponent<EnemyTower>();
            if (enemyTower != null)
            {
                activeEnemies.Remove(enemyTower);
            }

            if (activeEnemies.Count == 0 && damageFromEnemyCoroutine != null)
            {
                StopCoroutine(damageFromEnemyCoroutine);
                damageFromEnemyCoroutine = null;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            towerUpgradeButton.SetActive(false); // Hide the tower upgrade button
        }
    }

    private IEnumerator ApplyDamageOverTime(EnemyTower tower, float damageRate, float interval)
    {
        isTakingDamage = true;

        while (tower.enemyTowerHealth > 0)
        {
            // Apply damage to the enemy tower's health
            tower.enemyTowerHealth -= (int)(damageRate * interval);
            Debug.Log("Enemy Tower Health: " + tower.enemyTowerHealth);

            // Wait for the damage interval before applying damage again
            yield return new WaitForSeconds(interval);
        }

        // The tower's health reached or went below 0, destroy the entire enemy GameObject
        Destroy(tower.gameObject);

        isTakingDamage = false;
    }

    private IEnumerator ApplyDamageForTowerOverTime(float damageRate, float interval)
    {
        isTakingDamage = true;

        while (towerHealth > 0 && activeEnemies.Count > 0)
        {
            towerHealth -= damageRate * interval;
            Debug.Log("Player Tower Health: " + towerHealth);
            yield return new WaitForSeconds(interval);
        }

        isTakingDamage = false;
        damageFromEnemyCoroutine = null;
    }

    private void Update()
    {
        // Loop through all tower upgrade indicators and activate them based on the tower level
        for (int i = 0; i < towerUpgradeIndicator.Length; i++)
        {
            if (i < towerLevel)
            {
                towerUpgradeIndicator[i].SetActive(true);
            }
            else
            {
                towerUpgradeIndicator[i].SetActive(false);
            }
        }

        if (towerLevel == maxTowerLevel)
        {
            towerUpgradeButton.SetActive(false); // Hide the tower upgrade button
        }
    }

    public void TowerUpgrade()
    {
        // Check if the player has enough money to upgrade the tower
        if  (itemManager.itemAmount >= towerUpgradeCost && maxTowerLevel <= 10 )
        {
            itemManager.itemAmount -= towerUpgradeCost;
            towerLevel++; // Increase the tower level
            maxTowerHealth += towerUpgradeHealth; // Increase the tower health
            towerHealth+=5; // Reset the tower health to max health
        }
        else
        {
            Debug.Log("Not enough money to upgrade the tower");
        }
    }
}
