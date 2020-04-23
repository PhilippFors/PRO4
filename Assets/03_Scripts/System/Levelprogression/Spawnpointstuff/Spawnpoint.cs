using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "SpawnStuff/SpawnPoint")]
[System.Serializable]
public class SpawnPoint{
    public int Wave;
    public GameObject prefab;
    public GameObject Point;
    [HideInInspector] public Transform point =>Point.GetComponent<Transform>();
    [HideInInspector] public EnemyBody enemy => prefab.GetComponentInChildren<EnemyBody>();
}
