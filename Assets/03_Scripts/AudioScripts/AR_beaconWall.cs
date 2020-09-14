using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_beaconWall : MusicAnalyzer
{

    private Material _material;
    private float defaultLength;


   
    public float lengthOfLaser;


    Color _beaconEmissiveColor;
    float H, S, V;

    GameObject _energyWall;
    Color _energyWallColor;
    Material _energyWallMaterial;

    Sequence tweenSeq;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
       // _beaconEmissiveColor = _material.GetColor("EmissionRedColor");

        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        _energyWallMaterial = _energyWall.GetComponent<MeshRenderer>().material;
        //_energyWallColor = _energyWallMaterial.GetColor("EmissionRedColor");

        defaultLength = _energyWall.transform.localScale.y;


       // Color.RGBToHSV((_beaconEmissiveColor), out H, out S, out V);

        addActionToEvent();

        lengthOfLaser = lengthOfLaser /_energyWall.transform.localScale.y;


        EventSystem.instance.ActivateSkill += activateColorError1;
        EventSystem.instance.DeactivateSkill += deactivateColorError1;

    }

    // Update is called once per frame
    void Update() { 
    
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

            _energyWallMaterial.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            /*
            if (!m_holdHelper) {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
            */

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

            _energyWallMaterial.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            
            /*
            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
            */
        }
    }

    protected override void objectAction()
    {

        if (!colorErrorActive)
        {

            Debug.Log("Normal Barrier Action");
            increaseIntervalCounter();
            if (checkInterval())
            {
                tweenSeq = DOTween.Sequence()
               .Append(_energyWall.transform.DOScaleY(lengthOfLaser, m_actionInDuration))
               .Append(_energyWall.transform.DOScaleY(defaultLength, m_actionOutDuration))
           
               .SetEase(Ease.Flash);

                /**
                DOTween.Sequence()
                .Append(_energyWallMaterial.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", m_actionInDuration))
                .Join(_energyWall.transform.DOScaleY(lengthOfLaser, m_actionInDuration))
                .Append(_energyWallMaterial.DOColor(Color.HSVToRGB(H, S, 1, true), "EmissionRedColor", m_actionOutDuration))
                .Join(_energyWall.transform.DOScaleY(defaultLength, m_actionOutDuration))
                .SetEase(Ease.Flash);
                **/
            }

        }
    }


    public void activateColorError1(Skills skill)
    {
        if (skill.name == "PitchShift")
        {

            tweenSeq.Kill();
            Debug.Log("BeconBarrier activate");
            // colorErrorActive = true;

           

            tweenSeq = DOTween.Sequence()
            .Append(_energyWall.transform.DOScaleY(lengthOfLaser, m_actionInDuration))
            .SetEase(Ease.Flash);



        }
    }

    public void deactivateColorError1(Skills skill)
    {
        if (skill.name == "PitchShift")
        {

            Debug.Log("BeconBarrier deactivate");
            /*
            //colorErrorActive = false;
            DOTween.Sequence()
            .Join(_energyWall.transform.DOScaleY(defaultLength, m_actionInDuration))
            .SetEase(Ease.Flash);
            */
        }
    }
}
