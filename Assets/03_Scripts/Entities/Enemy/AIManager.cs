using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private void Update()
    {

    }
    public void SetAIActive()
    {
        foreach (EnemyBody enemy in SpawnManager.instance.enemyCollection)
        {
            enemy.GetComponent<StateMachineController>().SetAI(true);
        }
    }
}
