using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBody> durga = new List<EnemyBody>();
    [SerializeField] private List<EnemyBody> igner = new List<EnemyBody>();

    private void Start()
    {
        EventSystem.instance.addToAIManager += AddEnemy;
        EventSystem.instance.onEnemyDeath += RemoveEnemy;
    }

    private void OnDisable()
    {
        EventSystem.instance.addToAIManager -= AddEnemy;
        EventSystem.instance.onEnemyDeath -= RemoveEnemy;
    }

    void AddEnemy(EnemyBody enemy)
    {
        string tag = enemy.gameObject.tag;

        Debug.Log(tag);
        switch (tag)
        {
            case "Durga":
                durga.Add(enemy);
                break;
            case "Igner":
                igner.Add(enemy);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
    }

    void RemoveEnemy(EnemyBody enemy)
    {
        int toRemoveIndex;
        string tag = enemy.gameObject.tag;
        switch (tag)
        {
            case "Durga":
                toRemoveIndex = durga.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
                durga.RemoveAt(toRemoveIndex);
                break;
            case "Igner":
                toRemoveIndex = igner.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
                igner.RemoveAt(toRemoveIndex);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
    }




}
