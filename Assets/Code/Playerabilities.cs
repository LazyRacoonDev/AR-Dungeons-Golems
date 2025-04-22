using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
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

    [Header("Cooldown Settings")]
    public float ballCooldown = 1.5f;
    public float shieldCooldown = 5f;

    private bool canThrow = true;
    private bool canUseShield = true;
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
        if (!canThrow) return;

        if (ballPrefab && ballSpawnPoint)
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = ballSpawnPoint.forward * 10f;

            canThrow = false;
            Invoke(nameof(ResetThrow), ballCooldown);
        }
    }

    public void ActivateShield()
    {
        if (!canUseShield) return;

        if (shieldPrefab && shieldSpawnPoint)
        {
            currentShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation);
            Destroy(currentShield, 5f);

            canUseShield = false;
            Invoke(nameof(ResetShield), shieldCooldown);
        }
    }

    void ResetThrow()
    {
        canThrow = true;
    }

    void ResetShield()
    {
        canUseShield = true;
    }
}