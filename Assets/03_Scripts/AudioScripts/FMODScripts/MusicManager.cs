using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private bool m_kickActive = true;
    private bool m_snareActive = true;
    private bool m_highHatActive = true;

    public FMODUnity.StudioEventEmitter _emitter;

    // Start is called before the first frame update
    void Start()
    {

        EventSystem.instance.ActivateSkill += deactivateListener;
        EventSystem.instance.DeactivateSkill += activateListener;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SectionKickLaser(int sectionID)
    {
     
    }


    public void parseMarkerString(String marker)
    {
        if (marker[0] != '&')
        {
            for (int i = 0; i < marker.Length; i++)
            {
                switch (marker[i])
                {
                    case 'S':
                        //Debug.Log("Parsed: Kick");
                        if(EventSystem.instance == null)
                        {
                            Debug.Log("EventSystem is empty");
                        }
                        else
                        {

                            EventSystem.instance.OnSnare();
                        }                   
                        break;

                    case 'K':
                       // Debug.Log("Parsed: Bass");
                        if (EventSystem.instance == null)
                        {
                            Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            if (m_kickActive) { 
                            EventSystem.instance.OnKick();
                            }
                        }
                        break;
                    case 'H':
                        
                        if (EventSystem.instance == null)
                        {
                            Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            if (m_highHatActive) { 
                                EventSystem.instance.OnHighHat();
                            }
                        }
                        break;
                    case 'R':
                        _emitter.SetParameter("nextPart", 0);
                        break;
                }
            }
        }
        else
        {
            switch (marker)
            {
                case "&KL":
                    SectionKickLaser(1);
                    break;
            }
        }
    }

    public void deactivateListener(Skills skill)
    {
        if (skill.name == "LowPass")
        {
            m_highHatActive = false;
        }
        if (skill.name == "HighPass")
        {
            m_kickActive = false;
        }
    }

    public void activateListener(Skills skill)
    {
        if (skill.name == "LowPass")
        {
            m_highHatActive = true;
        }
        if (skill.name == "HighPass")
        {
            m_kickActive = true;
        }
    }

}
