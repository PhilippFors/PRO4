using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 3f;

    public static float EnemySpeed => Instance.enemySpeed;

    [SerializeField] private float enemyRange = 3f;
    public static float EnemyRange => Instance.enemyRange;

    [SerializeField] private float enemyLookSpeed= 3f;
    public static float EnemyLookSpeed => Instance.enemyLookSpeed;


    private static EnemySettings _instance;
    public static EnemySettings Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
