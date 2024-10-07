using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TreeLog : MonoBehaviour
{
    public GameObject log;
    public int logCount = 5;
    [Tooltip("The Scale at which the RNG will spread out logs")]
    [SerializeField] private float randScale = 1.0f;

    private List<GameObject> spawnedLogs = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        log = transform.Find("Log")?.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HarvestLog();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void HarvestLog()
    {
        if (logCount > 0)
        {
            float randNumber = Random.value * (float)3.75;
            Vector3 randPosition = new Vector3(transform.position.x * randNumber, transform.position.y, transform.position.z * randNumber);
            GameObject createdLog = Instantiate(log, transform.position, Quaternion.identity);
            spawnedLogs.Add(createdLog);

            // decrement the log count
            logCount--;
            Debug.Log("Harvesting Log. Logs Remaining..." + logCount);
        }
        else
        {
            Debug.Log("No Logs left. ");
        }
    }

    private void Reset()
    {
        logCount = 5;
       foreach(GameObject log in spawnedLogs)
        {
            Destroy(log);
        }

        spawnedLogs.Clear();
    }
}
