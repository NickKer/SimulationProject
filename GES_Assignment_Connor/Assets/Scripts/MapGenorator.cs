using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenorator : MonoBehaviour {
    public float partWidth = 10;
    public GameObject pathObject;
    public int PathAmount4 = 6;
    public int PathAmount3 = 6;
    public int PathAmount2 = 6;
    public int pathsPerSide = 2;
    int path4 = 0;
    int path3 = 0;
    int path2 = 0;
    pathObject[,] paths;
    int maxSides = 4;
    int pathType = 1;
    bool[] sides = new bool[4];
    GameObject currentPath;
    GameObject pathScript;
    // Use this for initialization
    void Start () {
        paths = new pathObject[pathsPerSide*2+1, pathsPerSide * 2+1];
        sides[0] = true;
        sides[1] = true;
        sides[2] = true;
        sides[3] = true;
        bool mapGenerate = true;
        float currentPositionX = 0;
        float currentPositionZ = 0;
        int[] requiredSides = new int[4] { 2, 2, 2, 2 };
        int start = pathsPerSide;
        int currentPosC = pathsPerSide;
        int currentPosR = pathsPerSide;
        int layer = 0;
        int dirC = 0;
        int dirR = 0;
        int count = 0;
        int pathCount = 4;
        int pathOutput;
        float timer;
        paths[currentPosR, currentPosC] = new pathObject();
        paths[currentPosR, currentPosC].generate(currentPositionX, currentPositionZ,partWidth,requiredSides,maxSides, currentPosR, currentPosC);
        //currentPositionX += gridx;
        requiredSides = new int[4] { 0, 0, 0, 0 };
        while (mapGenerate == true){
            if (layer == 0)
            {
                layer = 1;
                dirC = 0;
                dirR = 1;
            }
            else if ((start + layer) == currentPosR && dirC != 1)
            {
                dirR = 0;
                dirC = 1;
            }else if(((start + layer) == currentPosC && dirR != -1))
            {
                dirR = -1;
                dirC = 0;
            }
            else if (((start - layer) == currentPosR) && dirC != -1)
            {
                dirR = 0;
                dirC = -1;
            }
            else if (((start - layer) == currentPosC && dirR != 1))
            {
                dirR = 1;
                dirC = 0;
                layer += 1;
                
            }
            if (count == (pathsPerSide * 2+1) * (pathsPerSide * 2+1)-1 || pathCount - count == 0)
            {
                mapGenerate = false;
                break;
            }
            currentPositionX += dirC * partWidth;
            currentPositionZ += dirR * partWidth;
            currentPosC += dirC;
            currentPosR += dirR;
            if (PathAmount4 > path4)
            {
                maxSides = 4;
            }else if(PathAmount3 > path3)
            {
                maxSides = 3;
            }
            else if (PathAmount2 > path2)
            {
                maxSides = 2;
            }else
            {
                maxSides = 1;
            }
            //Debug.Log("");
            //Debug.Log(currentPosC);
            //Debug.Log(currentPosR);
            count += 1;
            if (currentPosR == 0)
            {
                requiredSides[1] = 0;
            }else if(paths[currentPosR-1, currentPosC] == null)
            {
                requiredSides[1] = 1;
            }
            else
            {
                requiredSides[1] = paths[currentPosR-1, currentPosC].sideNeeded(0);
            }
            if (currentPosR == pathsPerSide * 2)
            {
                requiredSides[0] = 0;
            }
            else if (paths[currentPosR+1, currentPosC] == null)
            {
                requiredSides[0] = 1;
            }
            else
            {
                requiredSides[0] = paths[currentPosR+1, currentPosC].sideNeeded(1);
            }
            if (currentPosC == 0)
            {
                requiredSides[3] = 0;
            }
            else if (paths[currentPosR, currentPosC-1] == null)
            {
                requiredSides[3] = 1;
            }
            else
            {
                requiredSides[3] = paths[currentPosR, currentPosC-1].sideNeeded(2);
            }
            if (currentPosC == pathsPerSide * 2)
            {
                requiredSides[2] = 0;
            }
            else if (paths[currentPosR, currentPosC+1] == null)
            {
                requiredSides[2] = 1;
            }
            else
            {
                requiredSides[2] = paths[currentPosR, currentPosC+1].sideNeeded(3);
            }
            paths[currentPosR, currentPosC] = new pathObject();
            pathOutput = paths[currentPosR, currentPosC].generate(currentPositionX, currentPositionZ,partWidth,requiredSides,maxSides,currentPosR,currentPosC);
            if (pathOutput == 4)
            {
                path4 += 1;
            }else if (pathOutput == 3)
            {
                path3 += 1;
            }else if (pathOutput == 2)
            {
                path2 += 1;
            }
            pathCount += pathOutput;
            //Debug.Log(" ");
            //Debug.Log(paths[currentPosC+1, currentPosR]);
            //Debug.Log(paths[currentPosC-1, currentPosR]);
            //Debug.Log(paths[currentPosC, currentPosR+1]);
            //Debug.Log(paths[currentPosC, currentPosR-1]);


        }
        Debug.Log("Paths Generated: " + count.ToString());
        Debug.Log("Paths 4: " + path4.ToString());
        Debug.Log("Paths 3: " + path3.ToString());
        Debug.Log("Paths 2: " + path2.ToString());
    }

    // Update is called once per frame

}

public class pathObject
{
    bool[] _sides = new bool[4];
    float[] _position = new float[2];
    float chance;
    float wallHeight = 5;
    int sideCount = 0;
    GameObject _floor;
    GameObject[] _path1;
    GameObject[] _path2;
    GameObject[] _path3;
    GameObject[] _path4;
    public int generate(float x, float z,float width,int[] sidesRequired,int maxSides,int row,int column)
    {
        _position[0] = x;
        _position[1] = z;
        for (int i = 0; i < 4; i++)
        {
            if (sidesRequired[i] == 0) {
                _sides[i] = false;
            }
            else if (sidesRequired[i] == 2)
            {
                _sides[i] = true;
                sideCount += 1;
            }
        }
        if (sideCount < maxSides && sideCount != 0)
        {
           // Debug.Log(sideCount);
            for (int i = 0; i < 4; i++){
                if (sidesRequired[i] != 0 && sidesRequired[i] != 2)
                {
                    chance = UnityEngine.Random.value;
                    if (chance > 0.3)
                    {
                        _sides[i] = true;
                        sideCount += 1;
                    }else
                    {
                        _sides[i] = false;
                    }
                    
                }
                //Debug.Log(_sides[i].ToString());
            }
        }
        if (sideCount != 0)
        {
            _floor = new GameObject();
            _floor.transform.position = new Vector3(x, 0, z);
            _floor.name = row.ToString() + " " + column.ToString();
            UnityEngine.AI.NavMeshObstacle tempObstacle;

            if (_sides[2] == true)
            {
                _path1 = new GameObject[4];
                //_path1[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path1[0].transform.position = new Vector3(x + width / 3, 0, z);
                //_path1[0].transform.localScale = new Vector3(width / 3, 1, width / 3);
                _path1[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.position = new Vector3(x + width / 3, width / 4, z + width / 6 + 0.5f);
                _path1[0].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path1[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path1[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[1].transform.position = new Vector3(x + width / 3, width / 4, z - width / 6 - 0.5f);
                _path1[1].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path1[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                //_path1[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path1[0].transform.parent = _floor.transform;
                _path1[0].transform.parent = _floor.transform;
                _path1[1].transform.parent = _floor.transform;
                //_path1[3].transform.parent = _floor.transform;

            }
            else
            {
                _path1 = new GameObject[2];
                _path1[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.position = new Vector3(x + width / 6 + 0.5f, width / 4, z);
                _path1[0].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path1[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path1[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.parent = _floor.transform;
                _path1[1].transform.parent = _floor.transform;
            }
            if (_sides[3] == true)
            {
                _path2 = new GameObject[4];
               // _path2[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
               // _path2[0].transform.position = new Vector3(x - width / 3, 0, z);
               // _path2[0].transform.localScale = new Vector3(width / 3, 1, width / 3);
                _path2[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.position = new Vector3(x - width / 3, width / 4, z + width / 6 + 0.5f);
                _path2[0].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path2[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path2[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[1].transform.position = new Vector3(x - width / 3, width / 4, z - width / 6 - 0.5f);
                _path2[1].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path2[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                //_path2[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path2[0].transform.parent = _floor.transform;
                _path2[0].transform.parent = _floor.transform;
                _path2[1].transform.parent = _floor.transform;
               // _path2[3].transform.parent = _floor.transform;

            }
            else
            {
                _path2 = new GameObject[2];
                _path2[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.position = new Vector3(x - width / 6 - 0.5f, width / 4, z);
                _path2[0].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path2[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path2[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.parent = _floor.transform;
                _path2[1].transform.parent = _floor.transform;
            }
            if (_sides[0] == true)
            {
                _path3 = new GameObject[4];
                //_path3[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path3[0].transform.position = new Vector3(x, 0, z + width / 3);
                //_path3[0].transform.localScale = new Vector3(width / 3, 1, width / 3);
                _path3[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.position = new Vector3(x + width / 6 + 0.5f, width / 4, z + width / 3);
                _path3[0].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path3[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path3[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[1].transform.position = new Vector3(x - width / 6 - 0.5f, width / 4, z + width / 3);
                _path3[1].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path3[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                // _path3[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // _path3[0].transform.parent = _floor.transform;
                _path3[0].transform.parent = _floor.transform;
                _path3[1].transform.parent = _floor.transform;
                //_path3[3].transform.parent = _floor.transform;

            }
            else
            {
                _path3 = new GameObject[2];
                _path3[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.position = new Vector3(x, width / 4, z + width / 6 + 0.5f);
                _path3[0].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path3[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path3[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.parent = _floor.transform;
                _path3[1].transform.parent = _floor.transform;
            }
            if (_sides[1] == true)
            {
                _path4 = new GameObject[4];
                //_path4[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path4[0].transform.position = new Vector3(x, 0, z - width / 3);
                //_path4[0].transform.localScale = new Vector3(width / 3, 1, width / 3);
                _path4[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.position = new Vector3(x + width / 6 + 0.5f, width / 4, z - width / 3);
                _path4[0].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path4[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path4[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[1].transform.position = new Vector3(x - width / 6 - 0.5f, width / 4, z - width / 3);
                _path4[1].transform.localScale = new Vector3(1, width / 2, width / 3);
                tempObstacle = _path4[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                //_path4[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path4[0].transform.parent = _floor.transform;
                _path4[0].transform.parent = _floor.transform;
                _path4[1].transform.parent = _floor.transform;
                //_path4[3].transform.parent = _floor.transform;

            }
            else
            {
                _path4 = new GameObject[2];
                _path4[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.position = new Vector3(x, width / 4, z - width / 6 - 0.5f);
                _path4[0].transform.localScale = new Vector3(width / 3, width / 2, 1);
                tempObstacle = _path4[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path4[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.parent = _floor.transform;
                _path4[1].transform.parent = _floor.transform;
            }
        }
        return sideCount;

    }

    public float[] position()
    {
        return _position;
    }
    public int sideNeeded(int side)
    {
        if(_sides[side] == true) {
            return 2;
        }else
        {
            return 0;
        }
    }
}
