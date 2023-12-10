using UnityEngine;

public class EnemyPrimitive : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints to move through
    public float moveSpeed = 5f; // Movement speed
    public float rotateSpeed = 10f; // Rotation speed

    private int currentWaypointIndex = 0; // Current waypoint index

    void Update()
    {
        if (waypoints.Length == 0) return;

        MoveToNextWaypoint();
        RotateTowardsWaypoint();
    }

    void MoveToNextWaypoint()
    {
        // Move towards the current waypoint's position
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // Check if the waypoint is reached
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Proceed to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void RotateTowardsWaypoint()
    {
        // Get the direction to the next waypoint
        Vector2 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Calculate rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate towards the direction
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
