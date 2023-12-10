using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the ItemManager component from the Player object
            ItemManager itemManager = collision.gameObject.GetComponent<ItemManager>();

            if (itemManager != null)
            {
                // Increment the itemAmount in the ItemManager
                itemManager.itemAmount++;

                // Destroy the collected item
                Destroy(gameObject);
                audioSource.Play();
            }
        }
    }
}
