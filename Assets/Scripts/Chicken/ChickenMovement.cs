using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChickenMovement : MonoBehaviour
{
    public float wanderSpeed, wanderRadius, timeTillWander, panicSpeed, panicLength;

    private Transform target;
    private NavMeshAgent agent;
    private float wanderCounter, panicCounter;
    public bool panic;
    
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        panic = false;
        wanderCounter = timeTillWander;
    }
    
    private void FixedUpdate()
    {
        if (panic)
        {
            panicCounter += Time.deltaTime;
            if (panicCounter >= panicLength)
            {
                panic = false;
                panicCounter = 0;
            }

            agent.speed = panicSpeed;
        }
        else
        {
            agent.speed = wanderSpeed;
        }

        //Wander logic
        wanderCounter += Time.deltaTime;
        if (wanderCounter >= timeTillWander)
        {
            Vector3 newPos = RandomNavDestination(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderCounter = 0;
        }
    }

    private static Vector3 RandomNavDestination(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void Startle(Vector3 threatLocation)
    {
        panic = true;
        panicCounter = 0;

        Transform startTransform = transform;

        transform.rotation = Quaternion.LookRotation(transform.position - threatLocation);
        Vector3 runTo = transform.position + transform.forward * panicSpeed;

        NavMeshHit navHit;
        NavMesh.SamplePosition(runTo, out navHit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        agent.SetDestination(navHit.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Startle(other.gameObject.transform.position);
        }

        if (other.gameObject.tag == "Food")
        {
            agent.SetDestination(other.gameObject.transform.position);
            Debug.Log("OE LEKKER");
        }

        if (other.gameObject.tag == "House")
        {
            agent.SetDestination(other.gameObject.transform.Find("Door").transform.position);
            Debug.Log("YEY NAAR HUIS");
        }
    }
}
