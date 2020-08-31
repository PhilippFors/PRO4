using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem.Controls;
using System.Net.Security;

public class AR_scaler : MusicAnalyzer
{

    public int _audioBand1 = 0;

    public float _sizeMinScale, _sizeMaxScale;
    public bool _enable32Bands;
    public float _multiply = 1;
    private float _multiplySave;
    public bool _invert;

    bool help = true;



    protected override void objectAction()
    {
       //
    }

    // Start is called before the first frame update
    void Start()
    {
        _multiply = _sizeMaxScale * 2;
        _multiplySave = _multiply;
        
        if (_invert)
        {
            float help = _sizeMinScale;
          //  _sizeMinScale = _sizeMaxScale;
           // _sizeMaxScale = help;
           // _multiply = -1 * _multiply;
        }

      
    }

    // Update is called once per frame
    void Update()
    {


        if (colorErrorActive)
        {

            _multiply *= 10;
        }
        else
        {
            _multiply = _multiplySave;
        }
    
        float currentScale = 0;
        if (_invert)
        {
            currentScale = _sizeMaxScale - FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1) * _multiply;
        }
        else
        {
            currentScale = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1) * _multiply;
        }
        
        if (currentScale > _sizeMaxScale) { 
            currentScale = _sizeMaxScale;
        }
        else if (currentScale < _sizeMinScale) {
            currentScale = _sizeMinScale;
        }
       // Vector3 testy = new Vector3(transform.localScale.x, currentScale, transform.localScale.z);

       // transform.localScale = testy;
        
            DOTween.Sequence()
            .Join(transform.DOScaleY(_sizeMinScale, 0.25f))
            .Join(transform.DOScaleY(currentScale, 0.1f))
            .SetEase(Ease.InFlash);
    }

    
}
