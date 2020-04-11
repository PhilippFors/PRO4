using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerControls input;

    public float lowPassSkill = 0;
    public float highPassSkill = 0;
    public float justSomeSkill = 0;
    public float lowPassSkillMax = 4;
    public float highPassSkillMax = 4;
    public float justSomeSkillMax = 4;
    
    private GameObject _child; //the weapon object
    public float comboCounter = 0;
    
    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    private void Awake()
    {
        input = new PlayerControls();

        input.Gameplay.LeftAttack.performed += rt => LeftAttack();
        input.Gameplay.RightAttack.performed += rt => RightAttack();
    }

    // Start is called before the first frame update
    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LeftAttack()
    {
        if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Wait"))
        {
            {
                _child.GetComponent<Animator>().SetTrigger("FastAttack");
            }
        }
    }
    
    public void RightAttack()
    {
        if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Wait"))
        {
            {
                _child.GetComponent<Animator>().SetTrigger("SlowAttack");
                
            }
        }
    }
}


