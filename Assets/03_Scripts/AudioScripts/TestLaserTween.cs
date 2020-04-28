using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;




public class TestLaserTween : MonoBehaviour
{


    public bool bassLaser;
    public bool kickLaser;

    Transform laserTrans;

    int beatCounter = 0;
    Vector3 turretLocation;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 turretLocation = transform.localPosition;
        laserTrans = gameObject.transform.GetChild(0);

        if (bassLaser)
        {
            EventSystem.instance.Bass += shootLaserSequence;
        }
        if (kickLaser)
        {
            EventSystem.instance.Kick += moveTurretOneWaySequence;
        }

       // moveTurretSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void shootLaser()
    {
        shootLaserSequence();
    }

    void laserEvent()
    {
        shootLaser();
    }



    void moveTurretSequence()
    {
        Ease ease = Ease.Linear;
        float originalLocation = transform.position.x;
        float duration = 2;
        DOTween.Sequence()
            .Append(transform.DOLocalMoveX(2, duration))
            .Append(transform.DOLocalMoveX(originalLocation, duration))
            .SetLoops(-1)
            .SetEase(ease);
    }

    void moveTurretOneWaySequence()
    {
        beatCounter++;
        Ease ease = Ease.Linear;
        float duration = 0.5f;
        if (beatCounter % 2 != 0)
        {
            DOTween.Sequence()
            .Append(transform.DOMoveX(transform.position.x - 5, duration))
            .SetEase(ease);
        }

        else
        {
            DOTween.Sequence()
            .Append(transform.DOMoveX(transform.position.x + 5, duration))
            .SetEase(ease);
        }
        
    }

    void shootLaserSequence()
    {
        float duration = 0.05f;
        DOTween.Sequence()
            .Append(laserTrans.transform.DOScaleY(10, duration))
            .Append(laserTrans.transform.DOScaleY(0.5f, duration));
    }



}
