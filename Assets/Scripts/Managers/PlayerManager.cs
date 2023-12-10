using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //Movement variables
    public float moveSpeed = 5f; // Movement speed of the astronaut
    public float rotateSpeed = 10f; // Rotation speed of the astronaut

    private Rigidbody2D rb;
    private Vector2 movement;

    //Health variables
    public int currentHealth;
    public int maxHealth = 30;

    public string enemyTargetTag;
    public Image healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Read input for movement in the horizontal and vertical axis
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on input
        movement = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Handle movement
        if (movement != Vector2.zero)
        {
            // Handle rotation
            HandleRotation();

            // Apply velocity for movement
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            // Gradually stop the player when no input is given
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.fixedDeltaTime * 5);
        }
    }

    void HandleRotation()
    {
        // Calculate the angle of movement
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        if (movement.x < 0)
        {
            // If moving left, add 180 degrees to the angle to flip the sprite
            angle += 180f;
        }

        // Smoothly rotate the astronaut towards the movement direction
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Adjust the sprite's local scale based on movement direction
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f); // Flip horizontally
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // Normal orientation
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTargetTag))
        {
            TakeDamage(10);
        }
        Debug.Log(this.gameObject.name + " has collided with " + collision.gameObject.name);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensuring health stays within bounds
        healthBar.fillAmount = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        TowersTracking.instance.Lose();
    }
}
