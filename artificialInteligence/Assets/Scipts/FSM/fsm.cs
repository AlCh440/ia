using UnityEngine;
using System.Collections;

public class fsm : MonoBehaviour
{

    float offset = 5;
    float radius = 3;

    GameObject[] ABOCHI;
    public float distSitting = 100f;
    //public move_03 moves;
    UnityEngine.AI.NavMeshAgent agent;

    private WaitForSeconds wait = new WaitForSeconds(0.05f); // == 1/20
    delegate IEnumerator State();
    private State state;

    GameObject Seatspot;

    IEnumerator Start()
    {

        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        ABOCHI = GameObject.FindGameObjectsWithTag("Seats");
        // moves = gameObject.GetComponent<move_03>();
       

        yield return wait;

        state = Wander;

        while (enabled)
        yield return StartCoroutine(state());
    }

    private void Update()
    {

        Seatspot = ABOCHI[0];
        
       // for (int i = 1; i < ABOCHI.Length; i++)
       // {
       //     if (Vector3.Distance(agent.transform.position, ABOCHI[i].transform.position) > distSitting)
       //     {
       //        Seatspot = ABOCHI[i];
       //     }
       // }

        Wander();

        if (state == Wander)
        {
            Wander();
        }
        else if (state == Approaching)
        {
            Approaching();
        }
        else if (state == Sittings)
        {
            Sittings();
        }




    }

    IEnumerator Wander()
    {
        Debug.Log("Wander state");

        while (Vector3.Distance(agent.transform.position, Seatspot.transform.position)> distSitting)
        {


           
            Wanders();
            yield return wait;
        };
        

        state = Approaching;
    }

    IEnumerator Approaching()
    {
        Debug.Log("Approaching state");

        agent.speed = 2f;        
        
        agent.destination = Seatspot.transform.position;

        bool sitting = false;
        while (Vector3.Distance(agent.transform.position, Seatspot.transform.position) < distSitting)
        {
            if (Vector3.Distance(Seatspot.transform.position, transform.position) < 0.5f)
            {
                sitting = true;
                break;
            };
            yield return wait;
        };

        if (sitting)
        {
            agent.speed = 0f;
            Debug.Log("Sitting");
            state = Sittings;
        }
        else
        {
            agent.speed = 1f;
            state = Wander;
        }
    }


    IEnumerator Sittings()
    {
        Debug.Log("Sittings");

        while (true)
        {
            agent.speed = 0f;
            yield return wait;
        };
    }


    void Wanders()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        agent.destination = worldTarget;
    }
}



