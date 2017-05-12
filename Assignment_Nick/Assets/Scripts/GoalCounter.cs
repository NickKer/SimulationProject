using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCounter : MonoBehaviour
{
    private Transform player;
    private Transform Ai;
    private Transform LoSAi;
    private Text countText;
    private int count;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Ai = GameObject.FindGameObjectWithTag("Enemy").transform;
        LoSAi = GameObject.FindGameObjectWithTag("EnemyLOS").transform;
        countText = GameObject.FindWithTag("CountText").GetComponent<Text>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(Ai.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(LoSAi.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Minion"))
        {
            ++count;
            
            SetCountText();


        }
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    }
