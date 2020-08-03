using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class SpawnProcess : MonoBehaviour
{   
    
    private Wave wave;
    public AIManager manager;
    private static int SpawnIndex;
    private List<SpawnPoint> Spawnpoints = new List<SpawnPoint>();
    public void StartSpawnAnim(Wave w)
    {
        // wave = null;
        // Spawnpoints.Clear();
        // SpawnIndex = 0;
        // wave = w;

        // foreach (SpawnPoint spawnPoint in wave.spawnPoints)
        // {
        //     spawnPoint.point.GetComponent<PlayableDirector>().Play();
        //     Spawnpoints.Add(spawnPoint);
        //     StartCoroutine(SpawnCO());
        // }
    }
    IEnumerator SpawnCO()
    {
        yield return new WaitForSeconds(0.5f);
        // EnemyBody enemy = Instantiate(Spawnpoints[SpawnIndex].prefab, Spawnpoints[SpawnIndex].point.position, Spawnpoints[SpawnIndex].point.transform.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
        // enemy.GetComponent<StateMachineController>().settings = manager;

        // enemy.GetComponent<Animation>().Play("Entry");
        // SpawnManager.instance.AddEnemyToList(enemy);
        // StartCoroutine(WaitForAnimation(enemy));
        // SpawnIndex++;
        // if (!SpawnManager.instance.count)
        //     SpawnManager.instance.count = true;
    }
    public void Spawn()
    {
        // EnemyBody enemy = Instantiate(Spawnpoints[SpawnIndex].prefab, Spawnpoints[SpawnIndex].point.position, Spawnpoints[SpawnIndex].point.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
        // enemy.GetComponent<StateMachineController>().settings = manager;
        // SceneManager.MoveGameObjectToScene(enemy.gameObject, SceneManager.GetSceneByName("AI_Prototype"));
        // enemy.GetComponent<Animation>().Play("Entry");
        // SpawnManager.instance.AddEnemyToList(enemy);
        // StartCoroutine(WaitForAnimation(enemy));
        // SpawnIndex++;
        // if (!SpawnManager.instance.count)
        //     SpawnManager.instance.count = true;
    }

    IEnumerator WaitForAnimation(EnemyBody enemy)
    {
        // while (enemy.gameObject.GetComponent<Animation>().isPlaying)
        // {
            yield return null;
        // }
        // EventSystem.instance.ActivateAI(enemy);
        // yield break;
    }



    
}
