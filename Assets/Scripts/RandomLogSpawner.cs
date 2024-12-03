using UnityEngine;

public class RandomLogSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject log;
    public Vector2 spawnAreaSize = new Vector2(10, 10);
    public int logSpawnCount = 0;

    
    
    void Start()
    {
        SpawnLogs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLogs()
    {
        for (int i = 0; i < logSpawnCount; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            Instantiate(log, randomPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        float randX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = 0;
        float randZ = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        return new Vector3(randX, y, randZ);
    }
}
