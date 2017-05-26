using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    public float distanceAway;
    public Transform thisObject;
    public int maxSpeedValue;
    private Transform player;
    private Collider playerCollider;
    private GameObject throwObject;
    private Collider throwObjectCollider;
    private NavMeshAgent navMeshAgent;
    public float range_radius = 10;
    public float timer;
    public float timer2;
    public int maxTimerValue;
    public int maxTimerValueOPAi;
    private int timerValue;
    public int movementRadius;
    private Vector3 currentPlayerPosition;
    private Vector3 currentThrowObjPosition;
    public Vector3 target;
    int i = 0;
    bool enemyFollowPlayer;



    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollider = player.GetComponent<Collider>();
        throwObject = GameObject.FindGameObjectWithTag("ThrowObject");
        throwObjectCollider = throwObject.GetComponent<Collider>();
        currentPlayerPosition = player.position;
        currentThrowObjPosition = FindNearestThrowObj();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        timerValue = Random.Range(2, maxTimerValue);
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
        //enemyFollowPlayer = false;
    }

   

    void Update()
    {
        Collider[] foundColliders = Physics.OverlapSphere(thisObject.position, range_radius);
        bool playerFound = false;

        timer += Time.deltaTime;

        if (timer >= timerValue)
        {
            Newtarget();
            timer = 0;
        }

        foreach (Collider coll in foundColliders)
        {
            if (currentPlayerPosition != player.position)
            {
                if (coll == playerCollider)
                    playerFound = true;
            } else if (currentPlayerPosition == player.position) {
                playerFound = false;
            }

            if (currentThrowObjPosition != FindNearestThrowObj())
            {
            
                if (coll.gameObject.CompareTag("ThrowObject"))
                {
                    
                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(FindNearestThrowObj());
                }
            }
            

        }
        if (playerFound)
        {
            if (player)
                {
                timer2 += Time.deltaTime;
                //enemyFollowPlayer = true;
                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(player.position);
                if (!navMeshAgent.pathPending)
                {
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    {
                        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            playerFound = false;
                        }
                    }
                }

                if (timer2 >= maxTimerValueOPAi)
                {
                    navMeshAgent.speed = maxSpeedValue * 3;
                    
                }

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
            } else { timer2 = 0; }

        
        currentPlayerPosition = player.position;
        currentThrowObjPosition = FindNearestThrowObj();
    }


void Newtarget ()
    {
        enemyFollowPlayer = false;
        Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, movementRadius, 1);
        Vector3 finalPosition = hit.position;
        GetComponent<NavMeshAgent>().destination = finalPosition;
        timerValue = Random.Range(2, maxTimerValue);
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
    }

    private Vector3 FindNearestThrowObj()
    {
        GameObject[] throwObject = GameObject.FindGameObjectsWithTag("ThrowObject");

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
