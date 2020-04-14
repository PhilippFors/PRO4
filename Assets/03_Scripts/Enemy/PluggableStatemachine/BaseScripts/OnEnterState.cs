using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnEnterState : ScriptableObject
{
    public abstract void Execute(StateMachineController contorller);
}
