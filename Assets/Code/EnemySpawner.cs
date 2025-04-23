using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;
    public GameObject enemyType4;
    public CanvasGameManager gameManager;

    public Transform spawnPoint;

    void Start()
    {
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(1, 5); // 1 to 4 inclusive

        GameObject enemyToSpawn = null;

        switch (randomIndex)
        {
            case 1:
                enemyToSpawn = enemyType1;
                break;
            case 2:
                enemyToSpawn = enemyType2;
                break;
            case 3:
                enemyToSpawn = enemyType3;
                break;
            case 4:
                enemyToSpawn = enemyType4;
                break;
        }
        

        if (enemyToSpawn != null && spawnPoint != null)
        {
            GameObject enemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
            enemy.GetComponent<EnemyCombat>().gameManager = gameManager; 
        }
    }
}