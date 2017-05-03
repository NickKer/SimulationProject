using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LoSEnemyAI : MonoBehaviour
{

    public float distanceAway;
    public GameObject thisObject;
    public int maxSpeedValue;
    public GameObject player;
    public Collider playerCollider;
    private NavMeshAgent navMeshAgent;
    public float range_radius = 10;
    public float timer;
    public float timer2;
    public float timer3;
    public int giveUpTime = 5;
    public int maxTimerValue;
    public int maxTimerValueOPAi;
    private int timerValue;
    public int movementRadius;
    public Vector3 target;
    private float height = 0.5f;
    bool playerFound;
    private Vector3 investigate;
    private Vector3 rayDirection;
    private Vector3 lastKnownPosition;
    private Vector3 currentThrowObjPosition;
    public GameObject throwObject;
    public Collider throwObjectCollider;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        timerValue = Random.Range(2, maxTimerValue);
        navMeshAgent.speed = Random.Range(3, maxSpeedValue);
        playerFound = false;
        throwObject = GameObject.FindGameObjectWithTag("Rock");
        currentThrowObjPosition = throwObject.transform.position;
    }

    void Update()
    {
        Collider[] foundColliders = Physics.OverlapSphere(thisObject.transform.position, range_radius);

        foreach (Collider coll in foundColliders)
        {
            if (currentThrowObjPosition != throwObject.transform.position)
            {
                if (coll == throwObjectCollider)
                {
                    navMeshAgent.speed = maxSpeedValue;
                    navMeshAgent.SetDestination(throwObject.transform.position);
                }
            }

        }

        timer += Time.deltaTime;

        if (timer >= timerValue)
        {
            Newtarget();
            timer = 0;
        }

        Investigate();

        if (playerFound)
        {
            Chase();
        }
        currentThrowObjPosition = throwObject.transform.position;
    }

    void Chase()
    {
        RaycastHit hit;
        rayDirection = (player.transform.position - transform.position).normalized;
            if (player)
            {
            timer3 += Time.deltaTime;
            navMeshAgent.speed = maxSpeedValue;
            if (timer3 >= maxTimerValueOPAi)
            {
                navMeshAgent.speed = maxSpeedValue * 3;

            }
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
                {
                if (hit.collider.gameObject.tag == "Player")
                {
                    navMeshAgent.SetDestination(player.transform.position);
                } else {
                    timer2 += Time.deltaTime;
                    navMeshAgent.SetDestination(player.transform.position);
                    timer = 0;
                    if (timer2 >= giveUpTime)
                    {
                        playerFound = false;
                        timer2 = 0;
                    }
                }
                    
                }
            }
            else
            {
                if (player = null)
                {
                    player = this.gameObject;
                }
                else
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }
            }
        }

    void Investigate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * height, transform.forward * range_radius, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * height, (transform.forward + transform.right).normalized * range_radius, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * height, (transform.forward - transform.right).normalized * range_radius, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * height, transform.forward, out hit, range_radius))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                playerFound = true;
                
                player = hit.collider.gameObject;
            }
        }


        if (Physics.Raycast(transform.position + Vector3.up * height, (transform.forward + transform.right).normalized, out hit, range_radius))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                playerFound = true;
                
                player = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * height, (transform.forward - transform.right).normalized, out hit, range_radius))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                playerFound = true;
                
                player = hit.collider.gameObject;
            }
        }

    }

    void Newtarget()
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
