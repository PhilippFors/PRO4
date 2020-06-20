﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnExitState : ScriptableObject
{
   public abstract void Execute(StateMachineController controller);
}
