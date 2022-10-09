using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTFlockManayer : MonoBehaviour
{


    public static WTFlockManayer FM;
    public GameObject prefabAgent;
    public int numAgents = 20;
    public GameObject[] allAgents;
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f);
    public Vector3 goalPos = Vector3.zero;

    [Header("Flocking Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed;
    [Range(0.0f, 5.0f)] public float maxSpeed;
    [Range(1.0f, 10.0f)] public float neighbourDistance;
    [Range(1.0f, 5.0f)] public float rotationSpeed;

    void Start()
    {

        allAgents = new GameObject[numAgents];

        for (int i = 0; i < numAgents; ++i)
        {

            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));

            allAgents[i] = Instantiate(prefabAgent, pos, Quaternion.identity);
        }

        FM = this;
        goalPos = this.transform.position;
    }


    void Update()
    {

        if (Random.Range(0, 100) < 10)
        {

            goalPos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
        }
    }
}