using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_beaconWall : MusicAnalyzer
{

    private Material _material;
    Material _energyWallMaterial;
    private float defaultLength;
    public float lengthOfLaser;
    GameObject _energyWall;
    Color _energyWallColor;
    Color _beaconEmissiveColor;
    float H, S, V;

    private bool m_beaconActive = true;



    Sequence tweenSeq;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        _energyWallMaterial = _energyWall.GetComponent<MeshRenderer>().material;

        defaultLength = _energyWall.transform.localScale.y;



        addActionToEvent();

        lengthOfLaser = lengthOfLaser / _energyWall.transform.localScale.y;


        EventSystem.instance.ActivateSkill += activateColorError1;
        EventSystem.instance.DeactivateSkill += deactivateColorError1;

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

            _energyWallMaterial.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));

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
               // shortDurationHelper();
                tweenSeq = DOTween.Sequence()
               .Append(_energyWall.transform.DOScaleY(lengthOfLaser, m_actionInDuration))
               .Append(_energyWall.transform.DOScaleY(defaultLength, m_actionOutDuration))
               .SetEase(Ease.Flash);
            }

        }
    }


    public void activateColorError1(Skills skill)
    {
        if (skill.name == "PitchShift")
        {

            tweenSeq.Kill();
            //Debug.Log("BeconBarrier activate");
            tweenSeq = DOTween.Sequence()
            .Append(_energyWall.transform.DOScaleY(lengthOfLaser, m_actionInDuration))
            .SetEase(Ease.Flash);



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
        Debug.Log("BeaconWallTrigger is pulled");
        if (m_beaconActive)
        {
            Debug.Log("BeaconWall hit");
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
}
