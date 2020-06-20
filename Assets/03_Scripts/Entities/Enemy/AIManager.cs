using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    public string playertag = "Player";
    public EnemySet set;
    public EnemySet durgaSet;
    public float minDistance;
    public Transform playerTarget;
    public float avoidDistance = 5f;
    public float selfAvoidDistance = 3f;
    public int rayAmount = 100;

    public float separationWeight = 0.5f;
    public float targetweight = 0.3f;

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

}

