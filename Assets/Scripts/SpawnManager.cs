using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float timeToSpawn = 2f;
    private readonly Vector3 spawnPosition = new(25f, 0f, 0f);
    
    [SerializeField] private GameObject obstaclePrefab;
    
    private PlayerController playerController;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke(nameof(SpawnObstacle), timeToSpawn);
    }

    void SpawnObstacle()
    {
        if (playerController.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }

        timeToSpawn = Random.Range(1f, 2f);
        Invoke(nameof(SpawnObstacle),timeToSpawn);
    }
}
