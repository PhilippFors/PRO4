using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_beaconWall : MusicAnalyzer
{
    public bool lengthChange = false;
    public bool isTurret;
    private Material _material;
    Material _energyWallMaterial;
    private float defaultLength;
    public float lengthOfLaser;
    GameObject _energyWall;
    Color _energyWallColor;
    Color _beaconEmissiveColor;
    float H, S, V;

    private bool m_beaconActive = true;

    public bool m_activateComponent = false;


    Sequence tweenSeq;


    public float dmgOnEnter = 30;
    public float dmgOnStay = 5;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        //_energyWallMaterial = _energyWall.GetComponent<MeshRenderer>().material;

        defaultLength = _energyWall.transform.localScale.y;

        if (m_activateComponent)
        {
            activateComponent();
        }

       
        if (!lengthChange)
        {
            lengthOfLaser = lengthOfLaser / _energyWall.transform.localScale.y;
        }

        


       

    }

    // Update is called once per frame
    void Update()
    {

        if (colorErrorActive && addedToEvent)
        {
            removeActionFromEvent();
        }
        else if (!colorErrorActive && !addedToEvent && m_activateComponent)
        {
            addActionToEvent();
        }

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
            foreach (Transform child in transform)
            {
                Debug.Log("Hallo");
                child.GetComponent<MeshRenderer>().material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, 10));
            }
            // _energyWallMaterial

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

            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, 5));
            }
        }
        
    }

    protected override void objectAction()
    {

        if (!colorErrorActive)
        {

            
            increaseIntervalCounter();
            if (checkInterval())
            {
               // shortDurationHelper();
               foreach (Transform child in transform)
                {
              
                   
                    tweenSeq = DOTween.Sequence()
                    .Append(child.DOScaleY(lengthOfLaser, m_actionInDuration))
                    .Append(child.DOScaleY(defaultLength, m_actionOutDuration))
                    .SetEase(Ease.Flash);
                }

            }

        }
    }


    public void activateColorError1(Skills skill)
    {
        if (skill.name == "PitchShift")
        {
            /*
           if (!isTurret)
            {
                tweenSeq.Kill();
            }
            */
            
            foreach (Transform child in transform)
            {
                tweenSeq.Complete();
                Sequence errorSequence = DOTween.Sequence()
            .Append(child.DOScaleY(lengthOfLaser, m_actionInDuration))
            .SetEase(Ease.Flash);
            }

        }
    }

    public void deactivateColorError1(Skills skill)
    {
        if (skill.name == "PitchShift")
        {
            //Debug.Log("BeconBarrier deactivate");
        }
    }


    public void PullTrigger(Collider c, float dmg)
    {
        //Debug.Log("BeaconWallTrigger is pulled");
        if (m_beaconActive)
        {
            //Debug.Log("BeaconWall hit");
            GameObject obj = c.gameObject;
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                EventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), dmg);
               
            }

        }
    }

    public void shortDurationHelper()
    {
        StartCoroutine("enableDmgRoutine");
    }

    IEnumerator enableDmgRoutine()
    {

        m_beaconActive = true;
        //gameObject.GetComponentInChildren<AR_beaconWallCollider>().EnableSelf();

        yield return new WaitForSeconds(m_actionInDuration + m_actionOutDuration);
        m_beaconActive = false;
        //gameObject.GetComponentInChildren<AR_beaconWallCollider>().DisableSelf();
    }


    public void activateComponent()
    {
        m_activateComponent = true;
        addActionToEvent();
        EventSystem.instance.ActivateSkill += activateColorError1;
        EventSystem.instance.DeactivateSkill += deactivateColorError1;
    }

    public void deactivateComponent()
    {
        m_activateComponent = false;
        removeActionFromEvent();
        EventSystem.instance.ActivateSkill -= activateColorError1;
        EventSystem.instance.DeactivateSkill -= deactivateColorError1;
    }
}
