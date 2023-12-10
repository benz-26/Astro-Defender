using UnityEngine;
using UnityEngine.UI;

public class EnemyTower : MonoBehaviour
{
    public Transform waypoint; // Array of waypoints to move through
    public float moveSpeed = 5f; // Movement speed
    public float rotateSpeed = 10f; // Rotation speed of the player
    public int enemyTowerHealth = 10;
    public string enemyTowerName;
    public Image enemyTowerHP;

    private void Start()
    {
        waypoint = GameObject.Find(enemyTowerName).transform;
    }

    private void Update()
    {
        MoveToNextWaypoint();
        RotateTowardsWaypoint();
        UpdateHealthBar();
    }

    void MoveToNextWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint.position, moveSpeed * Time.deltaTime);
    }

    void RotateTowardsWaypoint()
    {
        // Get the direction to the next waypoint
        Vector2 direction = (waypoint.position - transform.position).normalized;

        // Calculate rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate towards the direction
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    // Function to apply damage to the tower
    public void TakeDamage(int damage)
    {
        if (enemyTowerHealth > 0)
        {
            enemyTowerHealth -= damage;
            if (enemyTowerHealth <= 0)
            {
                enemyTowerHealth = 0; // Ensure health doesn't go below zero
            }
        }
    }

    // Function to update the health bar
    private void UpdateHealthBar()
    {
        if (enemyTowerHP != null)
        {
            // Calculate fill amount based on current health and maximum health (assuming max health is 10)
            float fillAmount = (float)enemyTowerHealth / 10f;
            enemyTowerHP.fillAmount = fillAmount;
        }
    }
}
