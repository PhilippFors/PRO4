using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicAnalyzer : MonoBehaviour
{


    public bool m_onSnare;
    public bool m_onKick;
    public bool m_onHighHat;

    public bool m_intervalBeat;
    public int m_interval = 2;
    public int m_intervalCounter = 0;

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
            m_intervalCounter++;
        }
        
    }

    protected bool checkInterval()
    {
        return (m_intervalCounter % m_interval == 0);
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
