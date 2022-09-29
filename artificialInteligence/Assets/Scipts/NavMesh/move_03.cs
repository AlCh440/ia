using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move_03 : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public bool seek;
    public bool flee;
    public bool wander;
    float maxVelocity = 10;
    float turnSpeed = 10;

    Vector3 movement;



    float turnAcceleration = 3f;
    float maxTurnSpeed = 7;
    float movSpeed;
    float acceleration = 3f;
    float maxSpeed = 7;

    float stopDistance = 10;
    float offset = 3;
    float radius = 5;

    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <
        stopDistance) return;
        if (seek)
        {
            Seek();
        }
        else if(flee)
        {
            Flee();
        }
        else if (wander)
        {
            Wander();
        }
        


        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxSpeed);

        //actual movement 
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                          rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed *
                              Time.deltaTime;
    }

    void Seek()
    {
        agent.destination = target.transform.position;
       
    }

    void Flee()
    {
        Vector3 fleeVector = target.transform.position - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
    }
}
