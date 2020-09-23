﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Avik/Decision/StunnedDec")]
public class AvikStunnedDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (!controller.stunned)
            return true;
        else
            return false;

    }
}
