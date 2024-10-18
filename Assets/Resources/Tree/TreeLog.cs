using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeLog : MonoBehaviour
{
    public GameObject log;
    private Camera playerCamera;
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
        switch (Input.inputString)
        {
            case "e":
                HarvestLog();
                break;
            case "r":
                Reset();
                break;
        }
    }

    private void HarvestLog()
    {
        if (logCount > 0)
        {
            // place the tree logs somewhere around the tree in a random offset
            float randAngle = Random.Range(0f, 2f * Mathf.PI);
            float randDistance = Random.Range(0f, randScale);

            float offsetX = randDistance * Mathf.Cos(randAngle);
            float offsetZ = randDistance * Mathf.Sin(randAngle);
            Vector3 randPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z + offsetZ);
            GameObject createdLog = Instantiate(log, randPosition, Quaternion.identity);
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

    private bool IsLookingAtLog()
    {
        Camera playerCamera = Camera.main;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == log.transform)
            {
                return true;
            }
        }
        return false;
    }

}