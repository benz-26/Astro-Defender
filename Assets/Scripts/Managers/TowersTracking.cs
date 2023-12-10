using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowersTracking : MonoBehaviour
{
    public static TowersTracking instance;

    [SerializeField]
    private GameObject[] towers;
    [SerializeField]
    private Image[] towersHealth;

    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelWin;

    private bool isGameOver = false;

    private PlayerManager playerManager;

    [SerializeField] GameObject panelPause;

    public bool chanceWinActive; // Enable/disable the chance-based win feature
    public int chanceWinLevel; // Level number for which the chance-based win is active

    private void Start()
    {
        instance = this;

        // Initialize game over panels
        panelGameOver.SetActive(false);
        panelWin.SetActive(false);
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }   
    }

    private void Update()
    {
        if (!isGameOver)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                TowerManager towerManager = towers[i].GetComponent<TowerManager>();

                if (towerManager.towerHealth <= 0 || playerManager.GetComponent<PlayerManager>().currentHealth <= 0)
                {
                    towersHealth[i].fillAmount = 0;
                    isGameOver = true;
                    Lose();
                    return; // Exit the method to prevent further execution after the game is over
                }
                else
                {
                    float fillAmount = towerManager.towerHealth / 100f; // Assuming max health is 100
                    towersHealth[i].fillAmount = fillAmount;
                }
            }

            // Check if all towers are at max level for regular win condition
            if (AreAllTowersMaxLevel())
            {
                Win();
                return; // Exit the method to prevent further execution after winning
            }

            if (chanceWinActive && SceneManager.GetActiveScene().buildIndex == chanceWinLevel)
            {
                if (!isGameOver)
                {
                    CheckChanceWinCondition();
                }
            }
        }
    }

    private void CheckChanceWinCondition()
    {
        int chance = Random.Range(1, 13345); // 1 in 13344 chance

        if (chance == 1)
        {
            Win();
        }
        else
        {
            Lose();
        }

        isGameOver = true;
    }


    private bool AreAllTowersMaxLevel()
    {
        foreach (GameObject tower in towers)
        {
            TowerManager towerManager = tower.GetComponent<TowerManager>();
            if (towerManager == null || towerManager.towerLevel < towerManager.maxTowerLevel)
            {
                return false;
            }
        }
        return true;
    }


    public void Lose()
    {
        // Display the game over panel
        panelGameOver.SetActive(true);
        // Perform any other game over actions (e.g., show scores, restart option, etc.)
        Debug.Log("Game Over");
    }


    public void Win()
    {
        // Display the game over panel
        panelWin.SetActive(true);
        // Perform any other game over actions (e.g., show scores, restart option, etc.)
        Debug.Log("You Win");
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void BackLevel(int scene)
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        LevelManager.instance.LoadLevel(scene);
    }


    public void SceneLoader(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
