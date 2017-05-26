﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenorator : MonoBehaviour
{
    public int difficulty;
    //these are to change based on difficulty:
    public float enemyChance = 0.66f;
    public float friendlyChance = 0.33f;
    public int friendlyMaxAmount = 10;
    public int enemyMaxAmount = 10;
    public int PathAmount4 = 6;
    public int PathAmount3 = 6;
    public int PathAmount2 = 6;
    public int pathsPerSide = 2;

    
    //pre-set values
    public float partWidth = 10;
    public float wallHeight = 5;
    public GameObject[] spawnableObjects;
    public GameObject[] homeTree;
    public Texture[] wallTextures;
    bool enemyAllowed = true;
    bool friendlyAllowed = true;
    int path4 = 0;
    int path3 = 0;
    int path2 = 0;
    pathObject[,] paths;
    int maxSides = 4;
    int pathType = 1;
    bool[] sides = new bool[4];
    GameObject currentPath;
    GameObject pathScript;

    public void getDifficulty()
    {
        difficulty = difficultyHolder.difficulty;
        Debug.Log(difficulty);
        setDifficulty();
    }
     void setDifficulty() {


            if (difficulty == 0)
            {
                
                enemyChance = 0.33f;
                friendlyChance = 0.33f;
            //friendly max needs to stay the same
                friendlyMaxAmount = 10;
                enemyMaxAmount = 3;
                PathAmount4 = 3;
                PathAmount3 = 3;
                PathAmount2 = 3;
                pathsPerSide = 2;
                Debug.Log("work");

            }

            else if (difficulty == 1)
            {
                enemyChance = 0.66f;
                friendlyChance = 0.33f;
                friendlyMaxAmount = 10;
                enemyMaxAmount = 10;
                PathAmount4 = 6;
                PathAmount3 = 6;
                PathAmount2 = 6;
                pathsPerSide = 2;
            Debug.Log("work1");

        }

            else if (difficulty == 2)
            {
                enemyChance = 0.66f;
                friendlyChance = 0.33f;
                friendlyMaxAmount = 10;
                enemyMaxAmount = 20;
                PathAmount4 = 15;
                PathAmount3 = 15;
                PathAmount2 = 15;
                pathsPerSide = 2;
                Debug.Log("hi");
            

        }

            else
            {
                enemyChance = 0.66f;
                friendlyChance = 0.33f;
                friendlyMaxAmount = 10;
                enemyMaxAmount = 40;
                PathAmount4 = 30;
                PathAmount3 = 30;
                PathAmount2 = 30;
                pathsPerSide = 4;
			Debug.Log("hi2");

            }
        }
    
    void Start()
    {
        getDifficulty();
        paths = new pathObject[pathsPerSide * 2 + 1, pathsPerSide * 2 + 1];
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
        string pathOutput;
        int enemyCount = 0;
        int friendlyCount = 0;

        paths[currentPosR, currentPosC] = new pathObject();
        paths[currentPosR, currentPosC].generate(currentPositionX, currentPositionZ, partWidth, wallHeight, requiredSides, maxSides, homeTree, false, false, "row: " + currentPosR.ToString() + " col: " + currentPosC.ToString(), gameObject, enemyChance, friendlyChance, wallTextures);
        //currentPositionX += gridx;
        requiredSides = new int[4] { 0, 0, 0, 0 };
        while (mapGenerate == true)
        {
            if (friendlyCount >= friendlyMaxAmount)
            {
                friendlyAllowed = false;
            }
            if (enemyCount >= enemyMaxAmount)
            {
                enemyAllowed = false;
            }
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
            }
            else if (((start + layer) == currentPosC && dirR != -1))
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
            if (count == (pathsPerSide * 2 + 1) * (pathsPerSide * 2 + 1) - 1)
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
            }
            else if (PathAmount3 > path3)
            {
                maxSides = 3;
            }
            else if (PathAmount2 > path2)
            {
                maxSides = 2;
            }
            else
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
            }
            else if (paths[currentPosR - 1, currentPosC] == null)
            {
                requiredSides[1] = 1;
            }
            else
            {
                requiredSides[1] = paths[currentPosR - 1, currentPosC].sideNeeded(0);
            }
            if (currentPosR == pathsPerSide * 2)
            {
                requiredSides[0] = 0;
            }
            else if (paths[currentPosR + 1, currentPosC] == null)
            {
                requiredSides[0] = 1;
            }
            else
            {
                requiredSides[0] = paths[currentPosR + 1, currentPosC].sideNeeded(1);
            }
            if (currentPosC == 0)
            {
                requiredSides[3] = 0;
            }
            else if (paths[currentPosR, currentPosC - 1] == null)
            {
                requiredSides[3] = 1;
            }
            else
            {
                requiredSides[3] = paths[currentPosR, currentPosC - 1].sideNeeded(2);
            }
            if (currentPosC == pathsPerSide * 2)
            {
                requiredSides[2] = 0;
            }
            else if (paths[currentPosR, currentPosC + 1] == null)
            {
                requiredSides[2] = 1;
            }
            else
            {
                requiredSides[2] = paths[currentPosR, currentPosC + 1].sideNeeded(3);
            }
            paths[currentPosR, currentPosC] = new pathObject();
            pathOutput = paths[currentPosR, currentPosC].generate(currentPositionX, currentPositionZ, partWidth, wallHeight, requiredSides, maxSides, spawnableObjects, enemyAllowed, friendlyAllowed, "row: " + currentPosR.ToString() + " col: " + currentPosC.ToString(), gameObject, enemyChance, friendlyChance, wallTextures);
            if (pathOutput == "Friendly")
            {
                friendlyCount += 1;
            }
            else if (pathOutput == "Enemy")
            {
                enemyCount += 1;
            }
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
        //getDifficulty();
    }

    // Update is called once per frame

}
public class pathObject
{
    bool[] _sides = new bool[4];
    float[] _position = new float[2];
    float chance;
    int sideCount = 0;
    string pathType;
    float[,] limitsX;
    float[,] limitsZ;
    GameObject _floor;
    GameObject[] _path1;
    GameObject[] _path2;
    GameObject[] _path3;
    GameObject[] _path4;
    public string generate(float x, float z, float width, float wallHeight, int[] sidesRequired, int maxSides, GameObject[] spawnables, bool enemy, bool friendly, string name, GameObject mainObject, float enemyChance, float friendlyChance, Texture[] wallTextures)
    {
        limitsX = new float[5, 2] { { x - width / 6, x + width / 6 }, { x + width / 6, x + width / 2 }, { x - width / 2, x - width / 6 }, { x - width / 6 + 1f, x + width / 6 - 1f }, { x - width / 6 + 1f, x + width / 6 - 1f } };
        limitsZ = new float[5, 2] { { z - width / 6, z + width / 6 }, { z - width / 6 + 1f, z + width / 6 - 1f }, { z - width / 6 + 1f, z + width / 6 - 1f }, { z + width / 6, z + width / 2 }, { z - width / 6, z - width / 2 } };
        _position[0] = x;
        _position[1] = z;
        for (int i = 0; i < 4; i++)
        {
            if (sidesRequired[i] == 0)
            {
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
            for (int i = 0; i < 4; i++)
            {
                if (sidesRequired[i] != 0 && sidesRequired[i] != 2)
                {
                    chance = UnityEngine.Random.value;
                    if (chance > 0.3)
                    {
                        _sides[i] = true;
                        sideCount += 1;
                    }
                    else
                    {
                        _sides[i] = false;
                    }

                }
                //Debug.Log(_sides[i].ToString());
            }
        }
        if (sideCount != 0)
        {
            _floor = new GameObject(name);
            _floor.transform.position = new Vector3(x, 0, z);
            UnityEngine.AI.NavMeshObstacle tempObstacle;
            _floor.transform.parent = mainObject.transform;

            if (_sides[2] == true)
            {
                _path1 = new GameObject[3];
                _path1[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.position = new Vector3(x + width / 3, wallHeight / 2, z + width / 6 + 0.5f);
                _path1[0].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path1[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path1[0].AddComponent(typeof(BoxCollider));
                _path1[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[1].transform.position = new Vector3(x + width / 3, wallHeight / 2, z - width / 6 - 0.5f);
                _path1[1].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path1[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path1[1].AddComponent(typeof(BoxCollider));
                _path1[2] = new GameObject("Path1");
                _path1[2].transform.position = new Vector3(x + width / 3, 0, z);
                _path1[2].transform.parent = _floor.transform;
                _path1[0].transform.parent = _path1[2].transform;
                _path1[1].transform.parent = _path1[2].transform;
                addTextures(_path1, wallTextures);
                //_path1[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //_path1[3].transform.parent = _floor.transform;


            }
            else
            {
                _path1 = new GameObject[2];
                _path1[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.position = new Vector3(x + width / 6 + 0.5f, wallHeight / 2, z);
                _path1[0].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path1[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path1[0].AddComponent(typeof(BoxCollider));
                _path1[1] = new GameObject("Path1");
                _path1[1].transform.position = new Vector3(x + width / 3, 0, z);
                _path1[1].transform.parent = _floor.transform;
                //_path1[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path1[0].transform.parent = _path1[1].transform;
                addTextures(_path1, wallTextures);
                //_path1[1].transform.parent = _path1[2].transform;
            }
            if (_sides[3] == true)
            {
                _path2 = new GameObject[3];
                _path2[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.position = new Vector3(x - width / 3, wallHeight / 2, z + width / 6 + 0.5f);
                _path2[0].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path2[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path2[0].AddComponent(typeof(BoxCollider));
                _path2[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[1].transform.position = new Vector3(x - width / 3, wallHeight / 2, z - width / 6 - 0.5f);
                _path2[1].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path2[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path2[1].AddComponent(typeof(BoxCollider));
                _path2[2] = new GameObject("Path2");
                _path2[2].transform.position = new Vector3(x - width / 3, 0, z);
                _path2[2].transform.parent = _floor.transform;
                //_path2[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.parent = _path2[2].transform;
                _path2[1].transform.parent = _path2[2].transform;
                addTextures(_path2, wallTextures);
                // _path2[3].transform.parent = _floor.transform;

            }
            else
            {
                _path2 = new GameObject[2];
                _path2[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.position = new Vector3(x - width / 6 - 0.5f, wallHeight / 2, z);
                _path2[0].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path2[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path2[0].AddComponent(typeof(BoxCollider));
                _path2[1] = new GameObject("Path2");
                _path2[1].transform.position = new Vector3(x - width / 3, 0, z);
                _path2[1].transform.parent = _floor.transform;
                //_path2[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path2[0].transform.parent = _path2[1].transform;
                addTextures(_path2, wallTextures);
                //_path2[1].transform.parent = _path2[2].transform;
            }
            if (_sides[0] == true)
            {
                _path3 = new GameObject[3];
                _path3[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.position = new Vector3(x + width / 6 + 0.5f, wallHeight / 2, z + width / 3);
                _path3[0].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path3[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path3[0].AddComponent(typeof(BoxCollider));
                _path3[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[1].transform.position = new Vector3(x - width / 6 - 0.5f, wallHeight / 2, z + width / 3);
                _path3[1].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path3[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path3[1].AddComponent(typeof(BoxCollider));
                _path3[2] = new GameObject("Path3");
                _path3[2].transform.position = new Vector3(x, 0, z + width / 3);
                _path3[2].transform.parent = _floor.transform;
                // _path3[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.parent = _path3[2].transform;
                _path3[1].transform.parent = _path3[2].transform;
                addTextures(_path3, wallTextures);
                //_path3[3].transform.parent = _floor.transform;

            }
            else
            {
                _path3 = new GameObject[2];
                _path3[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.position = new Vector3(x, wallHeight / 2, z + width / 6 + 0.5f);
                _path3[0].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path3[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path3[0].AddComponent(typeof(BoxCollider));
                _path3[1] = new GameObject("Path3");
                _path3[1].transform.position = new Vector3(x, 0, z + width / 3);
                _path3[1].transform.parent = _floor.transform;
                //_path3[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path3[0].transform.parent = _path3[1].transform;
                addTextures(_path3, wallTextures);
                // _path3[1].transform.parent = _path3[2].transform;
            }
            if (_sides[1] == true)
            {
                _path4 = new GameObject[3];
                _path4[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.position = new Vector3(x + width / 6 + 0.5f, wallHeight / 2, z - width / 3);
                _path4[0].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path4[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path4[0].AddComponent(typeof(BoxCollider));
                _path4[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[1].transform.position = new Vector3(x - width / 6 - 0.5f, wallHeight / 2, z - width / 3);
                _path4[1].transform.localScale = new Vector3(1, wallHeight, width / 3);
                tempObstacle = _path4[1].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path4[1].AddComponent(typeof(BoxCollider));
                _path4[2] = new GameObject("Path4");
                _path4[2].transform.position = new Vector3(x, 0, z - width / 3);
                _path4[2].transform.parent = _floor.transform;
                //_path4[3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.parent = _path4[2].transform;
                _path4[1].transform.parent = _path4[2].transform;
                addTextures(_path4, wallTextures);
                //_path4[3].transform.parent = _floor.transform;

            }
            else
            {
                _path4 = new GameObject[2];
                _path4[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.position = new Vector3(x, wallHeight / 2, z - width / 6 - 0.5f);
                _path4[0].transform.localScale = new Vector3(width / 3, wallHeight, 1);
                tempObstacle = _path4[0].AddComponent(typeof(UnityEngine.AI.NavMeshObstacle)) as UnityEngine.AI.NavMeshObstacle;
                tempObstacle.carving = true;
                _path4[0].AddComponent(typeof(BoxCollider));
                _path4[1] = new GameObject("Path4");
                _path4[1].transform.position = new Vector3(x, 0, z - width / 3);
                _path4[1].transform.parent = _floor.transform;
                //_path4[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _path4[0].transform.parent = _path4[1].transform;
                addTextures(_path4, wallTextures);
                //_path4[1].transform.parent = _path4[2].transform;
            }
            generateSpawnables(_floor, spawnables, enemy, friendly, limitsX, limitsZ, enemyChance, friendlyChance);
        }


        return pathType;

    }

    public float[] position()
    {
        return _position;
    }
    public void addTextures(GameObject[] objects, Texture[] textures)
    {
        Renderer renderer;
        if (textures.Length > 0)
        {
            for (int currentObject = 0; currentObject < objects.Length; currentObject++)
            {
                renderer = objects[currentObject].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.SetTexture("_MainTex", textures[Random.Range(0, textures.Length - 1)]);
                }
            }
        }
    }
    public void setHomeTree()
    {
        //make home
    }
    public void generateSpawnables(GameObject parent, GameObject[] spawnables, bool enemyAllowed, bool friendlyAllowed, float[,] xLimits, float[,] zLimits, float enemyChance, float friendlyChance)
    {
        float chance = UnityEngine.Random.value;
        float xT;
        float zT;
        int selectedPath;
        GameObject path;
        GameObject tempObject;
        float[] Perleftright = new float[2];
        float[] Perupdown = new float[2];
        if (chance < enemyChance && enemyAllowed == true)
        {
            pathType = "Enemy";
        }
        else if (chance < enemyChance + friendlyChance && friendlyAllowed == true)
        {
            pathType = "Friendly";
        }
        else
        {
            pathType = "Terrain";
        }
        for (int spawnArray = 0; spawnArray < spawnables.Length; spawnArray++)
        {
            objectData current = spawnables[spawnArray].GetComponent<objectData>();

            if (current.objectType == "Terrain" || current.objectType == pathType)
            {
                for (int objects = 0; objects < Random.Range(0, current.maxAmount); objects++)
                {
                    Perleftright = new float[2];
                    Perupdown = new float[2];
                    selectedPath = UnityEngine.Random.Range(0, 4);
                    if (selectedPath == 1 && _sides[2] == true)
                    {
                        System.Array.Copy(current.PercentageUpDown, Perleftright, 2);
                        System.Array.Copy(current.PercentageLeftRight, Perupdown, 2);
                        path = _path1[2];
                    }
                    else if (selectedPath == 2 && _sides[3] == true)
                    {
                        System.Array.Copy(current.PercentageLeftRight, Perupdown, 2);
                        Perleftright[0] = current.PercentageUpDown[1];
                        Perleftright[1] = current.PercentageUpDown[0];
                        path = _path2[2];
                    }
                    else if (selectedPath == 3 && _sides[0] == true)
                    {
                        System.Array.Copy(current.PercentageLeftRight, Perleftright, 2);
                        System.Array.Copy(current.PercentageUpDown, Perupdown, 2);
                        path = _path3[2];
                    }
                    else if (selectedPath == 4 && _sides[1] == true)
                    {
                        System.Array.Copy(current.PercentageLeftRight, Perleftright, 2);
                        Perupdown[0] = current.PercentageUpDown[1];
                        Perupdown[1] = current.PercentageUpDown[0];
                        path = _path4[2];
                    }
                    else
                    {
                        Perleftright = new float[2] { 0, 100 };
                        Perupdown = new float[2] { 0, 100 };
                        path = parent;
                    }

                    xT = UnityEngine.Random.Range((xLimits[selectedPath, 1] - xLimits[selectedPath, 0]) / 2.0f * Perleftright[0] / 100f, (xLimits[selectedPath, 1] - xLimits[selectedPath, 0]) / 2.0f * Perleftright[1] / 100f);
                    zT = UnityEngine.Random.Range((zLimits[selectedPath, 1] - zLimits[selectedPath, 0]) / 2.0f * Perupdown[0] / 100f, (zLimits[selectedPath, 1] - zLimits[selectedPath, 0]) / 2.0f * Perupdown[1] / 100f);

                    tempObject = Object.Instantiate(spawnables[spawnArray], path.transform.position + new Vector3(xT * RandomSign(), current.height, zT * RandomSign()), Quaternion.identity, path.transform);
                    tempObject.name = xT.ToString() + " " + zT.ToString();
                }
            }

        }
    }
    public int sideNeeded(int side)
    {
        if (_sides[side] == true)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
    public int RandomSign()
    {
        if (Random.value < 0.5)
        {
            return -1;
        }
        else
        {
            return 1;
        }

      }

    

}
