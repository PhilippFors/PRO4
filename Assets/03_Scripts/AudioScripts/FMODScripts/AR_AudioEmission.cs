using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_AudioEmission : AR_ColorMaster
{

    public int _audioBand1 = 0;
    public int _audioBand2 = 7;

    public bool _enableBlueFQChannel = true; //BLUE
    public bool _enableLilaFQChannel = false; //BLUE
    public bool _blueToMidFQ = false;
    public bool _enableRedFQChannel = true; //RED
    public bool _redToMidFQ = false;

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

    float H1, S1, V1;
    float H2, S2, V2;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        //_color1 = _material.GetColor("EmissionBlueColor");
        //_color2 = _material.GetColor("EmissionRedColor");

        if (_audioBand1 < 3)
        {
            Color.RGBToHSV((m_blueChannelActiveColor), out H1, out S1, out V1);
        }
        else if (_audioBand1 >= 3 && _audioBand1 <= 5)
        {
            Color.RGBToHSV((m_bothChannelActiveColor), out H1, out S1, out V1);
        }
        else
        {
            Color.RGBToHSV((m_redChannelActiveColor), out H1, out S1, out V1);
        }

        if (_audioBand2 < 3)
        {
            Color.RGBToHSV((m_blueChannelActiveColor), out H2, out S2, out V2);
        }
        else if (_audioBand1 >= 3 && _audioBand1 <= 5)
        {
            Color.RGBToHSV((m_bothChannelActiveColor), out H2, out S2, out V2);
        }
        else
        {
            Color.RGBToHSV((m_redChannelActiveColor), out H2, out S2, out V2);
        }



    }

    // Update is called once per frame
    void Update()
    {


        if (_useBuffer)
        {

            if (_enableBlueFQChannel)
            {
                V1 = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand1);
                _material.SetColor("EmissionBlueColor", Color.HSVToRGB(H1, S1, V1, true));
            }
               
            if (_enableRedFQChannel)
            {
                V2 = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand2);
                _material.SetColor("EmissionRedColor", Color.HSVToRGB(H2, S2, V2, true));
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

