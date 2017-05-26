using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{

    public float distanceAway = 10;
    public Transform thisObject;
    public int maxSpeedValue;
    private Transform player;
    private Transform goal;
    private Collider playerCollider;
    private Collider goalCollider;
    private NavMeshAgent navMeshAgent;
    public float range_radius = 10;
    private int timerValue;
    private bool foundGoal = false;
    private int i = 0;
   




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollider = player.GetComponent<Collider>();
        goal = GameObject.FindGameObjectWithTag("ThrowObject").transform;
        goalCollider = goal.GetComponent<Collider>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
    }



    void Update()
    {
        Collider[] foundColliders = Physics.OverlapSphere(thisObject.position, range_radius);
        bool playerFound = false;

        foreach (Collider coll in foundColliders)
        {
            if (coll.gameObject.CompareTag("Goal"))
            {
                i = 1;
                    navMeshAgent.SetDestination(FindNearestThrowObj());
                    if (!navMeshAgent.pathPending)
                    {
                        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                        {
                            if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                            {
                                
                            }
                        }
                    }
            }
            else if (coll == playerCollider && i == 0)
            {
                playerFound = true;


            }
            if (playerFound)
            {
                if (player)
                {

                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(player.position);

                }
                else
                {
                    if (player = null)
                    {
                        player = this.gameObject.GetComponent<Transform>();
                    }
                    else
                    {
                        player = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                }
            }
        }

    }
    private Vector3 FindNearestThrowObj()
    {
        GameObject[] throwObject = GameObject.FindGameObjectsWithTag("Goal");

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
        foreach (GameObject throwobj in throwObject)
        {
            Vector3 directionToTarget = throwobj.transform.position - currentPosition;

            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = throwobj.transform;
            }
        }

        return bestTarget.position;
    }
}

