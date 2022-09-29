using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_01 : MonoBehaviour
{
    float maxVelocity = 10;
    float turnSpeed = 10;
    public Transform target;
    Vector3 movement;



    float turnAcceleration = 1f;
    float maxTurnSpeed = 2;
    float movSpeed;
    float acceleration = 3f;
    float maxSpeed = 4;

    float stopDistance = 10;

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
        
        Seek();


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
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        movement = direction.normalized * acceleration;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void Flee()
    {
        //flee
        Vector3 direction = transform.position - target.transform.position;
        direction.y = 0;

        movement = direction.normalized * maxVelocity;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
