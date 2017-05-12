using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectData : MonoBehaviour
{
    public float[] PercentageLeftRight = new float[2] { 0, 100 };
    public float[] PercentageUpDown = new float[2] { 0, 100 };
    public float height = 0f;
    public int maxAmount = 3;
    public string objectType = "Terrain";
    public float[] objectScale = new float[3] { 1, 1, 1 };
    GameObject gameObject;

    void Start()
    {
        if (objectType != "Enemy" || objectType != "Friendly")
        {
            transform.localScale = new Vector3(objectScale[0], objectScale[1], objectScale[2]);
        }

    }
}
