using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TesteEvent : MonoBehaviour
{
    public event Action<SuperClass, float> OnActivation;
    private static TesteEvent _instance;
    public static TesteEvent instance
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

    public void Activation(SuperClass thing, float dmg)
    {
        OnActivation(thing, dmg);
        
    }
}
