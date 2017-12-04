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

        Vector3 direction = threatLocation - transform.position;
        agent.SetDestination(direction);
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
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
        }
    }
}
