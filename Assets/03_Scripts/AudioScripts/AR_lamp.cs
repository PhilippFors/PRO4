using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AR_lamp : MusicAnalyzer
{
    private Material m_material;

    public bool _useFrequency;
    public float m_frequencyMultiply;
    public int _audioBand;

    public float m_lightIntensityOn = 1000;
    public float m_lightIntensityOff = 0;
    public bool m_holdValue;
    bool m_holdHelper = true;
    Sequence mySequence;

    private bool m_plateActive = false;

    Color m_color;
    float H, S, V;
    float H1, S1, V1;
    float H2, S2, V2;


    private Light m_light;

    // Start is called before the first frame update
    void Start()
    {

        m_light = transform.GetComponentInChildren<Light>();
        if (!_useFrequency)
        {
            // activateComponent();
            _audioBand = -1;
        }

        if (_audioBand == 0)
        {
            m_light.color = m_blueChannelActiveColor;
        }
        else if (_audioBand == 7)
        {
            m_light.color = m_redChannelActiveColor;
        }
        else if (_audioBand >= 3 && _audioBand <= 5)
        {
            m_light.color = m_bothChannelActiveColor;
        }

        m_material = GetComponent<MeshRenderer>().material;
        m_startInterval = m_intervalCounter + 1;




    }

    // Update is called once per frame
    void Update()
    {
        if (colorErrorActive)
        {

            if (m_onKick || _audioBand == 0)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
                m_light.color = m_blueChannelActiveColor;
            }
            else if (m_onHighHat || _audioBand == 7)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
                m_light.color = m_redChannelActiveColor;
            }
            else if (m_onSnare || (_audioBand >= 3 && _audioBand <= 5))
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
                m_light.color = m_bothChannelActiveColor;
            }


            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, 10));
                //m_light.color = Color.HSVToRGB(H, S, 10);
            }

        }
        else
        {
            if (m_onKick || _audioBand == 0)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
                m_light.color = m_blueChannelActiveColor;
            }
            else if (m_onHighHat || _audioBand == 7)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
                m_light.color = m_redChannelActiveColor;
            }
            else if (m_onSnare || (_audioBand >= 3 && _audioBand <= 5))
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
                m_light.color = m_bothChannelActiveColor;
            }



            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, 10));
                // m_light.color = Color.HSVToRGB(H, S, 10);

            }
        }

        if (_useFrequency)
        {
            Debug.Log("HEY LISTEN");
            V = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand) * m_frequencyMultiply;
            m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));
            m_light.intensity = V;
        }
    }



    protected override void objectAction()
    {

        increaseIntervalCounter();

        if (checkInterval())
        {
            if (m_holdValue)
            {
                if (m_holdHelper)
                {

                    V = 10;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionBlueColor", m_actionInDuration))
                    .Join(m_light.DOIntensity(m_lightIntensityOn, m_actionInDuration))
                    .SetEase(Ease.Flash);
                    m_holdHelper = false;

                }
                else
                {

                    V = -10;
                    m_holdHelper = true;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionBlueColor", m_actionOutDuration))
                    .Join(m_light.DOIntensity(m_lightIntensityOff, m_actionInDuration))
                    .SetEase(Ease.Flash);



                }
            }
            else
            {
                mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionBlueColor", m_actionInDuration))
                    .Join(m_light.DOIntensity(m_lightIntensityOn, m_actionInDuration))
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionBlueColor", m_actionOutDuration))
                    .Join(m_light.DOIntensity(m_lightIntensityOff, m_actionInDuration))
                    .SetEase(Ease.Flash);
            }

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
    public void activateComponent(Scene scene, LoadSceneMode mode)
    {

        MyEventSystem.instance.ActivateSkill += onSkillActivated;
        MyEventSystem.instance.DeactivateSkill += onSkilldeactivated;
        MyEventSystem.instance.Deactivate += deactivateComponent;
    }

    public void deactivateComponent()
    {
        removeActionFromEvent();
        MyEventSystem.instance.ActivateSkill -= onSkillActivated;
        MyEventSystem.instance.DeactivateSkill -= onSkilldeactivated;
        MyEventSystem.instance.Deactivate -= deactivateComponent;
    }




}
