using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState
{
    private GameObject _child; //the weapon object

    public AttackState(PlayerStateMachine controller)
    {
        _child = controller.transform.GetChild(0).gameObject; //first child object of the player

    }

    public void StopMovement(PlayerStateMachine controller)
    {
        controller.currentMoveDirection = Vector3.zero;
    }
    public void Tick(PlayerStateMachine controller)
    {

        /*if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FastToIdle"))
        {
            controller.SetState(PlayerMovmentSate.standard);
        }*/
    }

}