using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicAnalyzer : AR_ColorMaster
{
    public bool m_onSnare;
    public bool m_onKick;
    public bool m_onHighHat;

    public bool m_onSkillActive;

    public bool m_intervalBeat;
    public int m_interval = 2;
    public int m_intervalCounter;
    public int m_startInterval;
    protected Boolean m_IntervalInvert = false;

    public float m_actionInDuration = 0.25f;
    public float m_actionOutDuration = 0.25f;


    bool test = false;



    // Start is called before the first frame update
    void Start()
    {
        
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void increaseIntervalCounter()
    {
        
        if (m_intervalBeat)
        {
            if (!colorErrorActive)
            {
                if (!test)
                {
                    test = true;
                }
                else
                {
                    m_intervalCounter++;
                }
               
                
            }
            else
            {
                if (test)
                {
                    test = false;
                }
                else
                {
                    m_intervalCounter--;
                }
              
            }

            
           // m_overallIntervalCounter++;
        }

    }

    protected bool checkInterval()
    {
        if (m_intervalBeat)
        {
            return (m_intervalCounter % m_interval == 0);
        }
        else
        {
            return true;
        }
    }

    protected abstract void objectAction();



    protected void addActionToEvent()
    {
         

        if (m_onSnare)
        {
            EventSystem.instance.Snare += objectAction;
        }

        if (m_onKick)
        {
            EventSystem.instance.Kick += objectAction;
        }

        if (m_onHighHat)
        {
            EventSystem.instance.HighHat += objectAction;
        }


    }

}
