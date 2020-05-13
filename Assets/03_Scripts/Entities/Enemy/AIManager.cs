using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;
using UnityEngine.AI;
public class AIManager : MonoBehaviour
{
    private void Start()
    {
        EventSystem.instance.activateAI += SetAIActive;
    }

    private void OnDisable() {
        EventSystem.instance.activateAI -= SetAIActive;
    }

    public void SetAIActive(EnemyBody enemy)
    {
        enemy.GetComponent<StateMachineController>().SetAI(true);

    }

    private void Update()
    {
        foreach (EnemyBody enemy in SpawnManager.instance.enemyCollection){
            //do something
        }
    }

}
