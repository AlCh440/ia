using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move_03 : MonoBehaviour
{
    //coding adventure: boids
    public NavMeshAgent agent;
    public GameObject target;

    public bool wander = false;
    public bool drunk = false;
    public bool pursue = false;
    public bool evade = false;
    public bool hide = false;
    public bool patrol = false;


    Vector3 movement;

    GameObject[] hidingSpots;
    public GameObject[] wayPoints;

    int patrolWP;

    
    float movSpeed;

    float stopDistance = 10;
    float offset = 5;
    float drunkOffset = 1f;
    float radius = 3;
    float drunkRadius = 2;

    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        if (hide)
            hidingSpots = GameObject.FindGameObjectsWithTag("hide");

        if (patrol)
        {
            patrolWP = (patrolWP + 1) % wayPoints.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Vector3.Distance(target.transform.position, transform.position) <
        stopDistance) return;

       
        if (wander)
        {
            if (agent.remainingDistance < 0.5f)
                Wander();
        }
        else if (drunk)
        {
            if (agent.remainingDistance < 0.5f)
                Drunk();
        }
        else if (pursue)
        {
            Pursue();
        }
        else if (evade)
        {
            Evade();
        }
        else if (hide)
        {
            Hide();
        }
        else if (patrol)
        {
            if (agent.remainingDistance < 0.5f) 
                patrolWP = (patrolWP + 1) % wayPoints.Length; 
            
            Patrol();
        }


       //turnSpeed += turnAcceleration * Time.deltaTime;
       //turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
       //movSpeed += acceleration * Time.deltaTime;
       //movSpeed = Mathf.Min(movSpeed, maxSpeed);
       //
       ////actual movement 
       //transform.rotation = Quaternion.Slerp(transform.rotation,
       //                                  rotation, Time.deltaTime * turnSpeed);
       //transform.position += transform.forward.normalized * movSpeed *
        //                      Time.deltaTime;
    }

    void Seek(Vector3 destination)
    {
        agent.destination = destination;
    
    }

    void Flee(Vector3 destination)
    {
        Vector3 fleeVector = destination - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
        
        agent.destination = worldTarget;
    }

    void Drunk()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * drunkRadius;
        localTarget += new Vector3(0, 0, drunkOffset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        agent.destination = worldTarget;
    }
    void Pursue()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / agent.speed;
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / agent.speed;
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    void Hide()
    {
        //GameObject hidingSpot = hidingSpots[0];
        //
        //for (int i = 1; i < hidingSpots.Length; i++)
        //{
        //    if ((hidingSpots[i].transform.position - transform.position).magnitude > (hidingSpot.transform.position - transform.position).magnitude)
        //    {
        //        hidingSpot = hidingSpots[i];
        //    }
        //}
        //
        //System.Func<GameObject, float> distance =
        //   (hs) => Vector3.Distance(target.transform.position,
        //                            hs.transform.position);
        //GameObject hidingSpot = hidingSpots.Select(
        //   ho => (distance(ho), ho)
        //   ).Min().Item2;
        //
        //Vector3 dir = hidingSpot.transform.position - target.transform.position;
        //Ray backRay = new Ray(hidingSpot.transform.position, -dir.normalized);
        //RaycastHit info;
        //hidingSpot.GetComponent<Collider>().Raycast(backRay, out info, 20f);
        //Vector3 destination = info.point + dir.normalized;
        //return destination;

    }

    void Patrol()
    { 
        Seek(wayPoints[patrolWP].transform.position);
        //agent.destination = wayPoints[patrolWP].transform.position;
    }
}

