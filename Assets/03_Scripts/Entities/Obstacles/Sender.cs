﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    Transform reciever;
    public bool active;
    [SerializeField] private Transform searcher;
    [SerializeField] private GameObject Wall;
    public ObstacleMaster master;
    public bool found = false;
    LayerMask recieverMask => LayerMask.GetMask("Reciever");
    public float minRot = -85f;
    public float maxRot = 85f;
    void Start()
    {
        Init();

        if (found)
            BuildWall();
    }

    void Init()
    {
        active = true;
        searcher.gameObject.SetActive(true);
        float i = 0;
        float j = 0;
        while (i <= maxRot || j >= minRot)
        {
            RaycastHit hit;
            Reciever obj;
            searcher.localEulerAngles = new Vector3(0, i, 0);
            if (Physics.Raycast(transform.position, searcher.forward, out hit, 4f, recieverMask))
            {
                obj = hit.transform.gameObject.GetComponent<Reciever>();
                if (obj != null)
                {
                    if (!obj.occupied)
                    {
                        obj.active = true;
                        reciever = obj.transform;
                        found = true;
                        obj.occupied = true;
                        master.nextReciever = obj;
                        return;
                    }
                }
            }

            searcher.localEulerAngles = new Vector3(0, j, 0);
            if (Physics.Raycast(searcher.position, searcher.forward, out hit, 4f, recieverMask))
            {
                obj = hit.transform.gameObject.GetComponent<Reciever>();
                if (obj != null)
                {
                    if (!obj.occupied)
                    {
                        reciever = obj.transform;
                        found = true;
                        obj.occupied = true;
                        obj.active = true;
                        master.nextReciever = obj;
                        return;
                    }
                }
            }
            i = i + 0.3f;
            j = j - 0.3f;
        }
        active = false;
        searcher.gameObject.SetActive(false);
    }

    void BuildWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(searcher.position, searcher.forward, out hit, 4f, LayerMask.GetMask("ObstacleMaster")))
        {
            if (hit.transform.gameObject.GetComponent<ObstacleMaster>())
                master.nextMaster = hit.transform.gameObject.GetComponent<ObstacleMaster>();
        }
        searcher.gameObject.SetActive(false);
        Vector3 dir = reciever.position - Wall.transform.position;
        Wall.SetActive(true);
        Wall.transform.rotation = Quaternion.LookRotation(dir);
        float distance = Vector3.Distance(reciever.position, Wall.transform.position);
        Wall.transform.localScale = new Vector3(Wall.transform.localScale.x, Wall.transform.localScale.y, 1);
        Wall.transform.localScale = new Vector3(Wall.transform.localScale.x, Wall.transform.localScale.y, distance);
    }
}
