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

    Color m_color;
    float H, S, V;
    float H1, S1, V1;
    float H2, S2, V2;

    private Material _material;
    public Color _color1;
    private Color _color2;


    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<MeshRenderer>().material;
        m_startInterval = m_intervalCounter + 1;

        BoxCollider a  = GetComponentInChildren<BoxCollider>();
        addActionToEvent();

        EventSystem.instance.ActivateSkill += onSkillActivated;
        EventSystem.instance.DeactivateSkill += onSkilldeactivated;
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
                    V = 10;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionInDuration))
                    .SetEase(Ease.Flash);
                    m_holdHelper = false ;
                }
                else
                {
                    V = -10;
                    m_holdHelper = true;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
                }
            }
            else
            {
                //Debug.Log("changPlateEmission");
                mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", m_actionInDuration))
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
            }
    
        }
    }


    public void PullTrigger(Collider c)
    {
        if (mySequence.IsActive())
        {
            Debug.Log("Damage Plate hit");
        }
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


}

