using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [HideInInspector] public string deathSceneName;

    #if UNITY_EDITOR
    [Header("Scene to load on death")]
    public SceneAsset deathScene; // Solo para editor
    #endif

    void Start()
    {
        currentHealth = maxHealth;

        #if UNITY_EDITOR
        if (deathScene != null)
        {
            deathSceneName = deathScene.name; // Guarda el nombre de la escena al iniciar
        }
        #endif

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
        SceneManager.LoadScene(deathSceneName);
    }
}