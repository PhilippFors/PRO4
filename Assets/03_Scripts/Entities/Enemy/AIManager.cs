using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;
using UnityEngine.AI;
public class AIManager : MonoBehaviour
{
    [SerializeField] private EnemyList enemyList;
    [SerializeField] private EnemyList durgaList;
    [SerializeField] private EnemyList ignerList;
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
        foreach (EnemyBody enemy in enemyList.entityList){
            //do something
        }
    }

}
