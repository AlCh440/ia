using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_02 : MonoBehaviour
{
    float maxVelocity = 4;
    float turnSpeed = 4;
    public Transform target;
    Vector3 movement;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Seek();


        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);


        //actual movement 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                                      Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;
    }

    void Seek()
    {
        //seek
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        movement = direction.normalized * maxVelocity;
    }

    void Flee()
    {
        //flee
        Vector3 direction = transform.position - target.transform.position;
        direction.y = 0;

        movement = direction.normalized * maxVelocity;
    }
}
