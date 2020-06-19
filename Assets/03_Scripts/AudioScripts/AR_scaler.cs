using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_scaler : MonoBehaviour
{

    public int _audioBand1 = 0;
    public int _audioBand2 = 7;


    public bool _useBuffer;
    public bool _colorChange;
    public bool _invertScale;
    public float _invertScaleValue;
    public float _sizeStartScale, _sizeMaxScale;
    public bool _enable32Bands;
    public float _multiply = 1;


    private int _bandAmount = 2;
    private Material _material;


    private Color _color1;
    private Color _color2;



    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;


        _color1 = _material.GetColor("EmissionBlueColor");
        _color2 = _material.GetColor("EmissionRedColor");

    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {

        if (_invertScale)
            {
                DOTween.Sequence()
               .Join(transform.DOScaleY(_invertScaleValue, 0.25f))
               .Join(transform.DOScaleY(FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1) * _multiply, 0.5f))
               .SetEase(Ease.Flash);
            }
        else
            {
                DOTween.Sequence()
                .Join(transform.DOScaleY(_sizeStartScale, 0.25f))
                .Join(transform.DOScaleY(FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1) * _multiply, 0.5f))
                .SetEase(Ease.Flash);
            }


            if (_colorChange)
            {
                float H, S, V;

                Color.RGBToHSV((_color1), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1);
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));

                Color.RGBToHSV((_color2), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand2);
                _material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, V, true));
            }

        }
        //IF NOT USE BUFFER
        else
        {
            float H, S, V;

            Color.RGBToHSV((_color1), out H, out S, out V);
            V = FMODAudioPeer._instance.getFqBand8(_audioBand1);
            _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));

            Color.RGBToHSV((_color2), out H, out S, out V);
            V = FMODAudioPeer._instance.getFqBand8(_audioBand2);
            _material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, V, true));


        }
    }
}
