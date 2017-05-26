using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCounter : MonoBehaviour
{
    private GameObject player;
    private GameObject[] Ai;
    private GameObject[] LoSAi;
    private GameObject[] Minion;
    private Text countText;
    private int count = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Ai = GameObject.FindGameObjectsWithTag("Enemy");
        LoSAi = GameObject.FindGameObjectsWithTag("EnemyLOS");
        Minion = GameObject.FindGameObjectsWithTag("Minion");
        countText = GameObject.FindWithTag("CountText").GetComponent<Text>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        foreach (GameObject ai in Ai)
        {
            Physics.IgnoreCollision(ai.GetComponent<Collider>(), GetComponent<Collider>());
        }

        foreach (GameObject ai in LoSAi)
        {
            Physics.IgnoreCollision(ai.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

 
    void OnCollisionEnter(Collision col)
    {
            if (col.gameObject.CompareTag("Minion"))
            {
                ++count;

                SetCountText();
                removeMinion();
            }
    }

    void SetCountText()
    {
        countText.text = "Kittens Saved: " + count.ToString();
    }

    private void removeMinion()
    {
        GameObject[] throwObject = GameObject.FindGameObjectsWithTag("Minion");

        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
        foreach (GameObject throwobj in throwObject)
        {
            Vector3 directionToTarget = throwobj.transform.position - currentPosition;

            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = throwobj;
            }
        }
        bestTarget.SetActive(false);
        
    }
}
