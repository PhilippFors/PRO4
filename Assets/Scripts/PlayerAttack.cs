using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject _child; //the weapon object

    // Start is called before the first frame update
    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
    }

    // Update is called once per frame
    void Update()
    {
        FastAttack();
        SlowAttack();
    }

    //fast attack: on button plays animation of child (weapon) object
    public void FastAttack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            _child.GetComponent<Animator>().SetTrigger("FastAttack");
        }
    }
    
    public void SlowAttack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            _child.GetComponent<Animator>().SetTrigger("SlowAttack");
        }
    }

   
}
