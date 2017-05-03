using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    public float distanceAway;
    public Transform thisObject;
    public int maxSpeedValue;
    public Transform player;
    public Collider playerCollider;
    public GameObject throwObject;
    public Collider throwObjectCollider;
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
   


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPlayerPosition = player.position;
        currentThrowObjPosition = throwObject.transform.position;
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        timerValue = Random.Range(2, maxTimerValue);
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
        throwObject = GameObject.FindGameObjectWithTag("Rock");
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

            if (currentThrowObjPosition != throwObject.transform.position)
            {
                if (coll == throwObjectCollider)
                {
                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(throwObject.transform.position);
                }
            }

        }
        if (playerFound)
        {
            if (player)
                {
                timer2 += Time.deltaTime;
                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(player.position);
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
            }

        
        currentPlayerPosition = player.position;
        currentThrowObjPosition = throwObject.transform.position;
    }


void Newtarget ()
    {
        Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, movementRadius, 1);
        Vector3 finalPosition = hit.position;
        GetComponent<NavMeshAgent>().destination = finalPosition;
        timerValue = Random.Range(2, maxTimerValue);
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
    }
}
