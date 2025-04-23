using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject abilityPrefab;
    public CanvasGameManager gameManager;

    public int maxHealth = 100;
   

    // Attack stuff
    public float attackInterval = 3f;
    public int lowDamage = 10;
    public int highDamage = 20;
    public float meleeDist = 5f;
    public float shootProb = 0.4f; 
    public float attackRange = 10f; 

    // Movement
    public float moveSpeed = 2.5f;

    private Transform playerTarget;
    private float currentHealth;
    private float attackTimer;
    private GameObject playerObj;
    private bool isAttacking; 

    void Start()
    {
        currentHealth = maxHealth;
        attackTimer = attackInterval;
        playerObj = GameObject.FindWithTag("Player");
        isAttacking = false; 

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

        float distance = Vector3.Distance(transform.position, playerTarget.position);
        //Debug.Log(distance);

        if (!isAttacking)
        {
            if (distance > attackRange)
            {
                MoveToPlayer();
            }
            else if (distance <= attackRange && distance > meleeDist)
            {
                float randomValue = Random.Range(0f, 2f);
                if (randomValue < shootProb)
                {
                    StartCoroutine(CastAbility());
                }
                else
                {
                    MoveToPlayer();
                }
            }
            else
            {
                int attackType = Random.Range(0, 2); // 0 for weak, 1 for strong
                if (attackType == 0)
                    StartCoroutine(AttackWeak());
                else
                    StartCoroutine(AttackStrong());
            }
        }
        

    }

    void MoveToPlayer()
    {
        if(playerTarget == null) return;

        Vector3 direction = playerTarget.position - transform.position;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;
        Debug.Log("Moving towards player");
    }

    IEnumerator CastAbility()
    {
        isAttacking = true;
        Debug.Log("Shoot");
        Instantiate(abilityPrefab, transform.position, transform.rotation);

        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    IEnumerator AttackWeak()
    {
        isAttacking = true;

        // Perform weak attack logic here
        Debug.Log("Weak Attack!");
        float Distance = Vector3.Distance(transform.position, playerTarget.position);
        if (Distance <= meleeDist)
        {
            playerTarget.GetComponent<PlayerHealth>().TakeDamage(lowDamage);
        }

        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    IEnumerator AttackStrong()
    {
        isAttacking = true;
        // Perform strong attack logic here
        Debug.Log("Strong Attack!");
        float Distance = Vector3.Distance(transform.position, playerTarget.position);
        if (Distance <= meleeDist)
        {
            playerTarget.GetComponent<PlayerHealth>().TakeDamage(highDamage);
        }
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameManager.ShowVictoryCanvas();
        Destroy(gameObject);
    }
}