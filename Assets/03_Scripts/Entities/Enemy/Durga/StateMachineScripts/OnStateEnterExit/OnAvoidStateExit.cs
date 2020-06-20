﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/OnStatEnter_Exit/AvoidExit")]
public class OnAvoidStateExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
       controller.checkedAmount = false;
    }
}
