using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabandDrop : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform Ai;
    public Transform LoSAi;
    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;

    void Start()
    {
        Physics.IgnoreCollision(Ai.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(LoSAi.GetComponent<Collider>(), GetComponent<Collider>());

    }

    

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }
   
    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
