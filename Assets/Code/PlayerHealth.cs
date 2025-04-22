using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);
        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
        // Add death logic here (e.g., respawn, game over screen, etc.)
        // For example, you could disable the player object:
        gameObject.SetActive(false);
    }
}
