using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;
using UnityEngine.AI;
public class AIManager : MonoBehaviour
{
    public List<StateMachineController> stateMachineList = new List<StateMachineController>();

    public bool useJobs = false;
    private void Start()
    {
        EventSystem.instance.addToStatemachineList += AddStateMachine;
        EventSystem.instance.onEnemyDeath += RemoveSateMachine;
    }

    private void OnDisable()
    {
        EventSystem.instance.onEnemyDeath -= RemoveSateMachine;
        EventSystem.instance.addToStatemachineList -= AddStateMachine;
    }
    private void Update()
    {
        float time = Time.realtimeSinceStartup;
        UpdatStateMachines();
        Debug.Log(((Time.realtimeSinceStartup - time)*1000f) + " ms");
    }

    void UpdatStateMachines()
    {
        if (stateMachineList.Count != 0)
            foreach (StateMachineController stateMachine in stateMachineList)
                stateMachine.Tick();

    }

    void RemoveSateMachine(EnemyBody enemy)
    {
        StateMachineController controller = enemy.gameObject.GetComponent<StateMachineController>();
        int toRemoveIndex = stateMachineList.FindIndex(x => x.GetComponent<StateMachineController>() == controller);
        stateMachineList.RemoveAt(toRemoveIndex);
    }
    void AddStateMachine(EnemyBody enemy)
    {
        stateMachineList.Add(enemy.gameObject.GetComponent<StateMachineController>());
    }
    public void SetAIActive()
    {
        foreach (EnemyBody enemy in SpawnManager.instance.enemyCollection)
        {
            enemy.GetComponent<StateMachineController>().SetAI(true);
        }
    }

}
