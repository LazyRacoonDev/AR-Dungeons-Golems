using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject abilityPrefab;
    public float attackInterval = 3f;
    public int maxHealth = 100;

    private Transform playerTarget;
    private int currentHealth;
    private float attackTimer;

    void Start()
    {
        currentHealth = maxHealth;
        attackTimer = attackInterval;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTarget != null)
        {
            transform.LookAt(playerTarget);
        }

        HandleCombat();
    }

    void HandleCombat()
    {
        if (playerTarget == null) return;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            CastAbility();
            attackTimer = attackInterval;
        }
    }

    void CastAbility()
    {
        if (abilityPrefab != null)
        {
            Instantiate(abilityPrefab, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}