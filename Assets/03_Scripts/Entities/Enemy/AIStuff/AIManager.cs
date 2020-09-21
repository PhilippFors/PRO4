﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public string searchPlayerTag = "Player";
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public LayerMask playerMask => LayerMask.GetMask("Player");
    public EnemySet allSet;
    public EnemySet avikSet;
    public EnemySet ralgerSet;
    public EnemySet shentauSet;
    public Transform playerTarget;
    public float avoidDistance = 5f;
    public float obstacleAvoidDistance = 2f;
    public float mainWhiskerL = 2f;
    public float secondaryWhiskerL = 1.5f;
    public float angleIncrement = 10f;
    public int whiskerAmount = 11;
    [Range(0, 100)]
    public int comboBias = 40;

    public float activationTime = 0.2f;
    private Vector3[] playerOffsetList;


    private void Start()
    {
        MyEventSystem.instance.activateAI += SetAIActive;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.activateAI -= SetAIActive;
    }

    public void SetAIActive(EnemyBody enemy)
    {
        StateMachineController st = enemy.GetComponent<StateMachineController>();
        st.SetAI(true);
        st.actions.Init();
    }

    private void Update()
    {
        if (allSet.entityList.Count == 0)
            return;

        GetCircleOffstets();
    }

    void GetCircleOffstets()
    {
        float currentAngle = 0;
        playerOffsetList = new Vector3[allSet.entityList.Count];
        foreach (EnemyBody enemy in allSet.entityList)
        {
            StateMachineController st = enemy.GetComponent<StateMachineController>();
            float x = 0;
            float z = 0;
            currentAngle += 360f / allSet.entityList.Count;
            float convAngle = currentAngle * Mathf.PI / 180F;
            x = (float)(4f * Mathf.Cos(convAngle) + playerTarget.position.x);
            z = (float)(4f * Mathf.Sin(convAngle) + playerTarget.position.z);
            st.offsetTargetPos = new Vector3(x, playerTarget.position.y, z);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     if (set.entityList.Count == 0 || set.entityList == null)
    //         return;

    //     foreach (Vector3 vec in playerOffsetList)
    //     {
    //         Gizmos.DrawWireSphere(vec, 0.2f);
    //     }
    // }

    public void FindPlayer()
    {
        if (FindObjectOfType<PlayerBody>() != null)
            playerTarget = FindObjectOfType<PlayerBody>().GetComponent<Transform>();
        else if (GameObject.FindGameObjectWithTag(searchPlayerTag) != null)
            playerTarget = GameObject.FindGameObjectWithTag(searchPlayerTag).GetComponent<Transform>();
        else
            Debug.LogError("Could not find Player!");
    }


}

