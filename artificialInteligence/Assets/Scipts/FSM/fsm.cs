using UnityEngine;
using System.Collections;

public class fsm : MonoBehaviour
{
   
    public GameObject seat;
    public float dist2Steal = 10f;
    move_03 move;
    UnityEngine.AI.NavMeshAgent agent;

    private WaitForSeconds wait = new WaitForSeconds(0.05f); // == 1/20
    delegate IEnumerator State();
    private State state;

    IEnumerator Start()
    {
        moves = gameObject.GetComponent<move_03>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        yield return wait;

        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    IEnumerator Wander()
    {
        Debug.Log("Wander state");

        while (Vector3.Distance(seat.transform.position)< distSitting)
        {
            moves.Wander();
            yield return wait;
        };

        state = Approaching;
    }

    IEnumerator Approaching()
    {
        Debug.Log("Approaching state");

        agent.speed = 2f;        
        moves.Seek(seat.transform.position);

        bool sitting = false;
        while (Vector3.Distance(seat.transform.position) > distsitting)
        {
            if (Vector3.Distance(seat.transform.position, transform.position) < 2f)
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
}

