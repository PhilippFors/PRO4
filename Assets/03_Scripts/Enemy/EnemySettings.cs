using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private float durgaSpeed = 3f;

    public static float DurgaSpeed => Instance.durgaSpeed;

    [SerializeField] private float durgaRange = 3f;
    public static float DurgaRange => Instance.durgaRange;

    [SerializeField] private float durgaTurnSpeed= 3f;
    public static float DurgaTurnSpeed => Instance.durgaTurnSpeed;



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
