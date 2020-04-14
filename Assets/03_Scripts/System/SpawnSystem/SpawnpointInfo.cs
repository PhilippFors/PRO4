using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointInfo : ScriptableObject
{
    public GameObject prefab;
    public Transform Spawnpoint;

    [HideInInspector] public EnemyBaseClass enemy => prefab.GetComponent<EnemyBaseClass>();
}
