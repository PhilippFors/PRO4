using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : ScriptableObject
{
    public GameObject prefab;
    public Transform Spawnpoint;

    [HideInInspector] public EnemyBody enemy => prefab.GetComponent<EnemyBody>();
}
