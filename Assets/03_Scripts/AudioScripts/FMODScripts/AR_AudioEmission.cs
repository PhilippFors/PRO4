using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_AudioEmission : MonoBehaviour
{

    public int _audioBand1 = 0;
    public int _audioBand2 = 7;


    public bool _useBuffer;
    public bool _colorChange;
    public bool _sizeChangeY;
    public float _sizeStartScale, _sizeMaxScale;
    public bool _enable32Bands;


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

