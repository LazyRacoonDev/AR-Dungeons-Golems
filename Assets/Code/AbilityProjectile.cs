using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    private GameObject player;
    private Vector3 targetDirection;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            targetDirection = (player.transform.position - transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = targetDirection * speed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
            }
            Destroy(gameObject);
        }
    }
}