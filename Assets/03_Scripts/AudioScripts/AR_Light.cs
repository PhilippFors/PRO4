using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_Light : MusicAnalyzer
{
    public bool _useFrequency;
    public int _audioBand;
    public float _startScale, _maxScale;
    
    public bool _highIntensityLight;
    Light _light;
    int _multiply;

    bool _enableListener;
    bool _bassListener;
    bool _kickListener;



    public bool m_holdValue;
    bool m_holdHelper = true;

    Sequence lightSequence;



    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        if (!_useFrequency)
        {
            _audioBand = -1;
        }

        if (_audioBand == 0)
        {
            _light.color = m_blueChannelActiveColor;
        }
        else if (_audioBand == 7)
        {
            _light.color = m_redChannelActiveColor;
        }



        if (_highIntensityLight)
        {
            _multiply = 10000;
        }
        else
        {
            _multiply = 1;

        }

        addActionToEvent();
    }

    // Update is called once per frame
    void Update()
    {

        if (colorErrorActive)
        {

            if (m_onKick || _audioBand == 0)
            {
                _light.color = m_blueChannelActiveColor;
            }
            else if (m_onHighHat || _audioBand == 7)
            {
                _light.color = m_redChannelActiveColor;
            }
            else if (m_onSnare)
            {
                _light.color = m_bothChannelActiveColor;
            }
            if (!m_holdHelper)
            {
                //
            }
        }
        else
        {
            if (m_onKick || _audioBand == 0)
            {
                _light.color = m_blueChannelActiveColor;
            }
            else if (m_onHighHat || _audioBand == 7)
            {
                _light.color = m_redChannelActiveColor;
            }
            else if (m_onSnare)
            {
                _light.color = m_bothChannelActiveColor;
            }
            // m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            if (!m_holdHelper)
            {
                //m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
        }

        if (_useFrequency)
        {
            _light.intensity = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand) * _multiply;
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
                    lightSequence = DOTween.Sequence()
                    .Append(_light.DOIntensity(1000, m_actionOutDuration))
                    .SetEase(Ease.Flash);
                    m_holdHelper = false;
                }
                else
                {
                    
                    m_holdHelper = true;
                    lightSequence = DOTween.Sequence()
                    .Append(_light.DOIntensity(0, m_actionOutDuration))
                    .SetEase(Ease.Flash);
                }
            }
            else
            {
                //Debug.Log("changPlateEmission");
                lightSequence = DOTween.Sequence()
                    .Append(_light.DOIntensity(1000, m_actionInDuration))
                    .Append(_light.DOIntensity(0, m_actionOutDuration))
                    .SetEase(Ease.Flash);
            }

        }
    }

}


