using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject abilityPrefab;
    

    public int maxHealth = 100;
   

    // Attack stuff
    public float attackInterval = 3f;
    public int lowDamage = 10;
    public int highDamage = 20;
    public float meleeDist = 5f;
    public float shootProb = 0.4f; 

    // Movement
    public float moveSpeed = 5f;

    private Transform playerTarget;
    private int currentHealth;
    private float attackTimer;
    private GameObject playerObj;
    private bool canAttack = true; 

    void Start()
    {
        currentHealth = maxHealth;
        attackTimer = attackInterval;
        playerObj = GameObject.FindWithTag("Player");

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
        Debug.Log(distance);

        if (canAttack)
        {
            // Check if the player is within melee range, if not, move towards the player or throw an ability
            if (distance > meleeDist)
            {
                float randomValue = Random.Range(0f, 1f);
                if(randomValue < shootProb)
                {
                    StartCoroutine(CastAbility());
                }
                else
                {
                    Vector3 direction = (playerTarget.position - transform.position).normalized;
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
                
            }
            else
            {
                int attack = Random.Range(0, 2);
                if (attack == 0)
                    StartCoroutine(AttackWeak());
                else
                    StartCoroutine(AttackStrong());
            }
                
        }       
    }

    IEnumerator CastAbility()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);

        Instantiate(abilityPrefab, transform.position, transform.rotation);

        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }

    IEnumerator AttackWeak()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);

        // Perform weak attack logic here
        Debug.Log("Weak Attack!");
        float Distance = Vector3.Distance(transform.position, playerTarget.position);
        if (Distance <= meleeDist)
        {
            playerTarget.GetComponent<PlayerHealth>().TakeDamage(lowDamage);
        }

        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }

    IEnumerator AttackStrong()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        // Perform strong attack logic here
        Debug.Log("Strong Attack!");
        float Distance = Vector3.Distance(transform.position, playerTarget.position);
        if (Distance <= meleeDist)
        {
            playerTarget.GetComponent<PlayerHealth>().TakeDamage(highDamage);
        }
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
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