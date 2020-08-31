using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_AudioEmission : AR_ColorMaster
{

    public int _audioBand1 = 0;
    public int _audioBand2 = 7;

    public bool _enableBlueFQChannel = true; //BLUE
    public bool _enableRedFQChannel = true; //RED

    public bool _useBuffer = true;
    public bool _enable32Bands;

    public static Color a  = Color.green;

    private int _bandAmount = 2;
   
    
    private Material _material;
    private Color _color1;
    private Color _color2;

    private Color _helpc1;
    private Color _helpc2;

    private Color _errorColor1 = Color.magenta;
    private Color _errorColor2 = Color.yellow;

    private bool test = false;



    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        //_color1 = _material.GetColor("EmissionBlueColor");
        //_color2 = _material.GetColor("EmissionRedColor");
  

    }

    // Update is called once per frame
    void Update()
    {


        if (_useBuffer)
        {

                float H, S, V;
            if (_enableBlueFQChannel)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1);
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));
            }

            if (_enableRedFQChannel)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand2);
                _material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, V, true));
            }

        }
        //IF NOT USE BUFFER
        else if (test)
        {
            float H, S, V;

            if (_enableBlueFQChannel)
            {
                Color.RGBToHSV((_color1), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBand8(_audioBand1);
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V, true));
            }

            if (_enableRedFQChannel)
            {
                Color.RGBToHSV((_color2), out H, out S, out V);
                V = FMODAudioPeer._instance.getFqBand8(_audioBand2);
                _material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, V, true));
            }
                

            
        }
        else
        {
            if (_enableBlueFQChannel)
            {
            
                _material.SetColor("EmissionBlueColor", m_blueChannelActiveColor);
            }

            if (_enableRedFQChannel)
            {
                
                _material.SetColor("EmissionRedColor", m_redChannelActiveColor);
            }
        }


    }

}

