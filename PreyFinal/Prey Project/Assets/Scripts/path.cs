using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    string[] sides;
    float chance; 
    // Use this for initialization
    void Start()
    {
        chance = Random.value;
        if(chance >= 0)
        {
            sides[0] = "top";
            sides[0] = "bottom";
            sides[0] = "left";
            sides[0] = "right";
        }
    }
}
