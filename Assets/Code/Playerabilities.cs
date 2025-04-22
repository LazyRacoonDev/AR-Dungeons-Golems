using UnityEngine;
using UnityEngine.UI;

public class AbilityControllerUI : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ballPrefab;
    public GameObject shieldPrefab;

    [Header("Spawn Points")]
    public Transform ballSpawnPoint;
    public Transform shieldSpawnPoint;

    [Header("UI Buttons")]
    public Button throwButton;
    public Button shieldButton;

    private GameObject currentShield;

    void Start()
    {
        if (throwButton != null)
            throwButton.onClick.AddListener(ThrowBall);

        if (shieldButton != null)
            shieldButton.onClick.AddListener(ActivateShield);
    }

    public void ThrowBall()
    {
        if (ballPrefab && ballSpawnPoint)
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = ballSpawnPoint.forward * 10f;

            Destroy(ball, 3f); // Destroy the ball after 2 seconds
        }
    }

    public void ActivateShield()
    {
        if (shieldPrefab && shieldSpawnPoint)
        {
            currentShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation);
            Destroy(currentShield, 1f);
        }
    }
}