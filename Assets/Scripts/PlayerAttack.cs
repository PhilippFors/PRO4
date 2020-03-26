﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int lowPassSkill = 0;
    private GameObject _child; //the weapon object
    public int comboCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();

    }

    
    
    public void Attacking()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            _child.GetComponent<Animator>().SetTrigger("FastAttack");
        }
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            _child.GetComponent<Animator>().SetTrigger("SlowAttack");
        }
    }
    
    


}
