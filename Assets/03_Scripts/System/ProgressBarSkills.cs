using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressBarSkills : ProgessBar
{
    public PlayerAttack player;

    //public int id = 0;
    public Skills skillObject;
    
    
    // Start is called before the first frame update
    void Start()
    { 
        
        player = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        //skillObject = player.skills[id];
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
        GetCurrentFill();
    }
    
    public override void GetCurrentFill()
    {
        
            maximum = (skillObject.max);
            current = (skillObject.current);
            base.GetCurrentFill();
        
      
           
        
       
    }
}
