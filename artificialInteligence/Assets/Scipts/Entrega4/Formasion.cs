using UnityEngine;
using UnityEngine.AI;

public class Formasion : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Vector3 pos;
    public Quaternion rot;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Leader");
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.TransformPoint(pos);
    }

    void Update()
    {
        agent.destination = target.transform.TransformPoint(pos);
    }
}
