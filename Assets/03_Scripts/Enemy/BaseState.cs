using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class BaseState
{
    public BaseState(GameObject gameobject, IEnemyBase temp)
    {
        this.gameObject = gameobject;
        this.transform = gameobject.transform;
        this.template = temp;
    }

    protected GameObject gameObject;
    protected Transform transform;
    protected IEnemyBase template;

    public abstract Type OnStateUpdate();

    public abstract void OnStateEnter();

    public abstract void OnStateExit();
}

