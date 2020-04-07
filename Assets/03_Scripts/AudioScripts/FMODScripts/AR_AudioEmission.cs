using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_AudioEmission : MonoBehaviour
{
    private FMODAudioPeer _audioPeer;
    public int[] _bandArray;
    // public float _startScale, _maxScale;
    public bool _useBuffer;

    public bool _colorCube;

    public bool _yDynamicCube;
    public float _yDynamicStartScale, _yDynamicMaxScale;



    public bool enable32Bands;
    public int[] _bandArray32;

    int _bandAmount = 2;
    Material _material;


    Color color1;
    Color color2;

    Color[] colorArray;
    int hitCounter;
    // Start is called before the first frame update
    void Start()
    {
        //TODO 
        //Einbauen dass nur Materials mit den Anfang ar in die Liste aufgenommen werden

        GameObject gameObj = GameObject.Find("FMODAudioPeer");
        _audioPeer = gameObj.GetComponent<FMODAudioPeer>();


        _bandAmount = _bandArray.Length;
        colorArray = new Color[_bandAmount];
        _material = GetComponent<MeshRenderer>().material;


        hitCounter = 0;

        colorArray[0] = _material.GetColor("EmissionBlueColor");
        colorArray[1] = _material.GetColor("EmissionRedColor");

    }

    // Update is called once per frame
    void Update()
    {

        if (enable32Bands)
        {
            transform.localScale = new Vector3(transform.localScale.x, (FMODAudioPeer._audioBandBuffer32[_bandArray[0]] * 20) + _yDynamicStartScale, transform.localScale.z);
        }

        if (_useBuffer)
        {
            if (_yDynamicCube)
            {
                if (FMODAudioPeer._audioBandBuffer8[_bandArray[0]] >= 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x, (FMODAudioPeer._audioBandBuffer8[_bandArray[0]] * _yDynamicMaxScale) + _yDynamicStartScale, transform.localScale.z);
                }
            }
            if (_colorCube)
            {
                float H, S, V;
               
                Color.RGBToHSV((colorArray[0]), out H, out S, out V);
                V = FMODAudioPeer._audioBandBuffer8[_bandArray[0]];
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));

                Color.RGBToHSV((colorArray[1]), out H, out S, out V);
                V = FMODAudioPeer._audioBandBuffer8[_bandArray[1]];
                _material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, V, true));  
            }

        }
        //IF NOT USE BUFFER
        else
        {
            float H, S, V;
            for (int i = 0; i < _bandAmount; i++)
            {
                Color.RGBToHSV((colorArray[i]), out H, out S, out V);
                V = FMODAudioPeer._audioBand8[_bandArray[i]];
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));
            }
        }
    }
}

