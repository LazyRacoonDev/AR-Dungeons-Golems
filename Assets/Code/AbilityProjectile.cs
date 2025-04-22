using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    public float speed = 10f;
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
}