using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
public class MusicManager : MonoBehaviour
{
   public enum MusicPart {Start, Mid, Break, ArenaFight, Ending};
   public MusicPart musicPart;
 

    private bool m_kickLock = false;
    private bool m_snareLock = false;
    private bool m_highHatLock = false;

    private bool m_kickSkillLock = false;
    private bool m_snareSkillLock = false;
    private bool m_highHatSkillLock = false;

    public FMODUnity.StudioEventEmitter _emitter;

    // Start is called before the first frame update
    void Start()
    {

        
        
        _emitter  = GetComponent<FMODUnity.StudioEventEmitter>();
        debugJumpToMusicPart();


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
                            // Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            
                            if (!m_snareLock)
                            {
                                EventSystem.instance.OnSnare();
                            }

                            lockInstrument("lockSnare");

                        }                   
                        break;

                    case 'K':
                       // Debug.Log("Parsed: Bass");
                        if (EventSystem.instance == null)
                        {
                            // Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            if (!m_kickLock && !m_kickSkillLock) { 
                                EventSystem.instance.OnKick();
                            }
                            lockInstrument("lockKick");
                        }
                        break;
                    case 'H':
                        
                        if (EventSystem.instance == null)
                        {
                            // Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            if (!m_highHatLock && !m_highHatSkillLock) { 
                                EventSystem.instance.OnHighHat();
                            }
                            lockInstrument("lockHighHat");
                        }
                        break;
                    case 'R':
                        _emitter.SetParameter("nextPart", 0);
                        break;
                    case 'X':
                        EventSystem.instance.OnDeactivate();
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
            m_highHatSkillLock = true;
        }
        if (skill.name == "HighPass")
        {
            m_kickSkillLock = true;
        }
    }

    public void activateListener(Skills skill)
    {
        if (skill.name == "LowPass")
        {
            m_highHatSkillLock = false;
            Debug.Log(m_highHatSkillLock);
        }
        if (skill.name == "HighPass")
        {
            m_kickSkillLock = false;
        }
    }

    public void lockInstrument(String lockName)
    {
        StartCoroutine(lockName);
    }

    IEnumerator lockSnare()
    {
        
        m_snareLock = true;
        yield return new WaitForSeconds(.1f);
        m_snareLock = false;
    }

    IEnumerator lockKick()
    {

        m_kickLock = true;     
        yield return new WaitForSeconds(.1f);
        m_kickLock = false;
    }

    IEnumerator lockHighHat()
    {

        m_highHatLock = true;
        yield return new WaitForSeconds(.1f);
        m_highHatLock = false;
    }

    void debugJumpToMusicPart()
    {
        switch (musicPart)
        {
            case MusicPart.Start:
                _emitter.SetParameter("PlayFrom", 0);
                break;
            case MusicPart.Mid:
                _emitter.SetParameter("PlayFrom", 1);
                break;
            case MusicPart.Break:
                _emitter.SetParameter("PlayFrom", 2);
                break;
            case MusicPart.ArenaFight:
                _emitter.SetParameter("PlayFrom", 3);
                break;
            case MusicPart.Ending:
                _emitter.SetParameter("PlayFrom", 4);
                break;
        }
    }

}
