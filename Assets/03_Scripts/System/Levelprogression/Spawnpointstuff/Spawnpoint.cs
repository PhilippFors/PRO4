using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// [CreateAssetMenu(menuName = "SpawnStuff/SpawnPoint")]
[System.Serializable]
public class SpawnPoint{
    public GameObject prefab;
    public GameObject Point;
    [HideInInspector] public Transform point =>Point.GetComponent<Transform>();
    [HideInInspector] public EnemyBody enemy => prefab.GetComponentInChildren<EnemyBody>();
}
