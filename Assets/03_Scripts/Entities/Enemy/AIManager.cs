using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public string searchPlayerTag = "Player";
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    public EnemySet set;
    public EnemySet durgaSet;
    public Transform playerTarget;
    public float avoidDistance = 5f;
    public float obstacleAvoidDistance = 2f;
    public float mainWhiskerL = 2f;
    public float secondaryWhiskerL = 1.5f;
    public float angleIncrement = 10f;
    public int whiskerAmount = 11;

    private void Start()
    {
        EventSystem.instance.activateAI += SetAIActive;
    }

    private void OnDisable()
    {
        EventSystem.instance.activateAI -= SetAIActive;
    }

    public void SetAIActive(EnemyBody enemy)
    {
        enemy.GetComponent<StateMachineController>().SetAI(true);
    }

    private void Update()
    {
        if (set.entityList.Count == 0)
            return;

        float currentAngle = 0;

        foreach (EnemyBody enemy in set.entityList)
        {
            StateMachineController st = enemy.GetComponent<StateMachineController>();

            float x = 0;
            float z = 0;
            currentAngle += 360f / set.entityList.Count;
            float convAngle = currentAngle * Mathf.PI / 180F;
            x = (float)(st.enemyStats.GetStatValue(StatName.Range) * Mathf.Cos(convAngle) + playerTarget.position.x);
            z = (float)(st.enemyStats.GetStatValue(StatName.Range) * Mathf.Sin(convAngle) + playerTarget.position.z);
            st.offsetTargetPos = new Vector3(x, playerTarget.position.y, z);
        }
    }

    private void OnDrawGizmos()
    {
        if (set.entityList.Count == 0)
            return;
        float currentAngle = 0;
        foreach (EnemyBody enemy in set.entityList)
        {
            StateMachineController st = enemy.GetComponent<StateMachineController>();

            float x = 0;
            float z = 0;
            currentAngle += 360f / set.entityList.Count;
            float convAngle = currentAngle * Mathf.PI / 180F;
            x = (float)(st.enemyStats.GetStatValue(StatName.Range) * Mathf.Cos(convAngle) + playerTarget.position.x);
            z = (float)(st.enemyStats.GetStatValue(StatName.Range) * Mathf.Sin(convAngle) + playerTarget.position.z);
            Gizmos.DrawWireSphere(new Vector3(x, playerTarget.position.y, z), 0.2f);
        }
    }

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

