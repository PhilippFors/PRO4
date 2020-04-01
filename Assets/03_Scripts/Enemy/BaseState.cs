using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class BaseState
{
    public BaseState(GameObject gameobject)
    {
        this.gameObject = gameobject;
        this.transform = gameobject.transform;
    }

    protected GameObject gameObject;
    protected Transform transform;

    public abstract Type OnStateUpdate();

    public abstract void OnStateEnter();

    public abstract void OnStateExit();
}

