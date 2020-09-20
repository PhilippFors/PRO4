using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_damagePlate : MusicAnalyzer
{
    private Material m_material;
    private float defaultLength;

    public bool m_holdValue;
    bool m_holdHelper = true;
    Sequence mySequence;

    private bool m_plateActive = false;

    Color m_color;
    float H, S, V;
    float H1, S1, V1;
    float H2, S2, V2;

    private Material _material;



    public float dmgOnEnter = 30;
    public float dmgOnStay = 5;

    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<MeshRenderer>().material;
        m_startInterval = m_intervalCounter + 1;

        BoxCollider a  = GetComponentInChildren<BoxCollider>();
        activateComponent();
    }

    // Update is called once per frame
    void Update()
    {
        if (colorErrorActive)
        {

            if (m_onKick)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
            }
            else if (m_onHighHat)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
            }
            else if (m_onSnare)
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
            }

            m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            if (!m_holdHelper) {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }

        }
        else
        {
            if (m_onKick)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
            }
            else if (m_onHighHat)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
            }
            else if (m_onSnare)
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
            }

            m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            
            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
        }
    }



    protected override void objectAction()
    {

        increaseIntervalCounter();
        
        if (checkInterval())
        {
            if (m_holdValue)
            {
                if (m_holdHelper) {
                    m_plateActive = true;
                    V = 10;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionInDuration))
                    .SetEase(Ease.Flash);
                    m_holdHelper = false ;
                    gameObject.GetComponentInChildren<Ar_damagePlateCollider>().EnableSelf();
                }
                else
                {
                  
                    V = -10;
                    m_holdHelper = true;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
                    m_plateActive = false;

                    gameObject.GetComponentInChildren<Ar_damagePlateCollider>().DisableSelf();
                }
            }
            else
            {
    
                shortDurationHelper();
                mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", m_actionInDuration))
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
            }
    
        }
    }


    public void PullTrigger(Collider c, float dmg)
    {
        bool hit = false;
        if (m_plateActive)
        {
        Debug.Log("Damage Plate hit");
        GameObject obj = c.gameObject;
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                EventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(),dmg);
                hit = true;
            }

        }
    }

    public void shortDurationHelper()
    {
        StartCoroutine("enableDmgRoutine");
    }

    IEnumerator enableDmgRoutine()
    {

        m_plateActive = true;
        gameObject.GetComponentInChildren<Ar_damagePlateCollider>().EnableSelf();
       
        yield return new WaitForSeconds(m_actionInDuration);
        m_plateActive = false;
        gameObject.GetComponentInChildren<Ar_damagePlateCollider>().DisableSelf();
    }


    public void onSkillActivated(Skills skill)
    {
        if (skill.name == "PitchShift")
        {
            if (!m_IntervalInvert)
            {
               //  m_intervalCounter = m_interval - m_startInterval;
                //m_intervalCounter = -1;
               // m_IntervalInvert = true;
            }

           
        } 
    }

    public void onSkilldeactivated(Skills skill)
    {
        if (skill.name == "PitchShift")
        {
            if (m_IntervalInvert)
            {
               // m_intervalCounter = m_interval + m_startInterval;
               // m_IntervalInvert = false;
            }
        }
    }
    public void activateComponent()
    {
        addActionToEvent();
        EventSystem.instance.ActivateSkill += onSkillActivated;
        EventSystem.instance.DeactivateSkill += onSkilldeactivated;
        EventSystem.instance.Deactivate += deactivateComponent;
    }

    public void deactivateComponent()
    {
        removeActionFromEvent();
        resetPlate();
        EventSystem.instance.ActivateSkill -= onSkillActivated;
        EventSystem.instance.DeactivateSkill -= onSkilldeactivated;
        EventSystem.instance.Deactivate -= deactivateComponent;
    }

    public void resetPlate()
    {
        V = -10;
        m_holdHelper = true;
        mySequence = DOTween.Sequence()
        .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionOutDuration))
        .SetEase(Ease.Flash);
        m_plateActive = false;

        gameObject.GetComponentInChildren<Ar_damagePlateCollider>().DisableSelf();
    }

}

