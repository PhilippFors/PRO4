using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCube : MonoBehaviour
{
   
    public int[] _bandArray;
   // public float _startScale, _maxScale;
    public bool _useBuffer;

    public bool _deathBlock;

    public bool _laser;
    public float _laserThreshhold;

    public bool _colorCube;

    public bool _yDynamicCube;
    public float _yDynamicFactor;

    public bool _useUnityAudioSystem;


    int _bandAmount;
    Material[] _materials;
    Material[] _arMaterials;

    Color color1;
    Color color2;

    Color[] colorArray;
    int hitCounter;
    // Start is called before the first frame update
    void Start()
    {
        //TODO 
        //Einbauen dass nur Materials mit den Anfang ar in die Liste aufgenommen werden


        _bandAmount = _bandArray.Length;
       
        colorArray = new Color[_bandAmount];
        _materials = GetComponent<MeshRenderer>().materials;
        _arMaterials = new Material[_materials.Length];

        int j = 0;
        for (int i = 0; i < _materials.Length; i++)
        {
          
            if (_materials[i].name.Substring(0, 2) == "ar")
            {
                _arMaterials[j] = _materials[i];
                j++;
            }

        }



        for (int i = 0; i < _bandAmount; i++)
        {
           if (_colorCube)
            {
                    colorArray[i] = _arMaterials[i].GetColor("_EmissionColor");
            }
           
        }
        hitCounter = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_useBuffer)
        {
            if (_useUnityAudioSystem)
            {
                float H, S, V;
                for (int i = 0; i < _bandAmount; i++)
                {
                    Color.RGBToHSV((colorArray[i]), out H, out S, out V);
                    V = AudioListenerPeer._audioBandBuffer[_bandArray[i]];
                    _arMaterials[i].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
                }
            }
           else
            {
                if (_yDynamicCube)
                {
                    if (FMODAudioPeer._audioBandBuffer[_bandArray[0]] >= 0)
                    {
                        if (_laser)
                        {
                            
                            if (FMODAudioPeer._audioBandBuffer[_bandArray[0]] > _laserThreshhold)
                            {
                                transform.localScale = new Vector3(transform.localScale.x, 50, transform.localScale.z);
                            }
                            else
                            {
                                transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
                            }
                            
                        }
                        else
                        {
                            transform.localScale = new Vector3(transform.localScale.x, FMODAudioPeer._audioBandBuffer[_bandArray[0]] * _yDynamicFactor, transform.localScale.z);
                        }
                    
                    }
                   
                }
                
                if (_colorCube)
                {
                    // NEW IMPLEMENTATION WITH FMOD AUDIO SYSTEm
                    float H, S, V;
                    for (int i = 0; i < _bandAmount; i++)
                    {
                        Color.RGBToHSV((colorArray[i]), out H, out S, out V);
                        V = FMODAudioPeer._audioBandBuffer[_bandArray[i]];
                        if (_laser)
                        {
                            Debug.Log("BandWeight:" +V);
                        }
                        _arMaterials[i].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
                    }
                }
                if (_laser)
                {

                }
            }
        }
        //IF NOT USE BUFFER
        else
        {
            // NEW IMPLEMENTATION WITH FMOD AUDIO SYSTEm
            float H, S, V;
            for (int i = 0; i < _bandAmount; i++)
            {
                Color.RGBToHSV((colorArray[i]), out H, out S, out V);
                V = FMODAudioPeer._audioBand[_bandArray[i]];
                _arMaterials[i + 1].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
            }

            //DEATH CUBE
            /*
             if (V > 0.7f)
             {
                 hitCounter++;
                 _material[1].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
                 _material[2].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
             }
             else
             {
                 _material[1].SetColor("_EmissionColor", Color.black);
                 _material[2].SetColor("_EmissionColor", Color.black);
             }

             //Color _color2 = new Color(AudioListenerPeer._audioBandBuffer[_band2], AudioListenerPeer._audioBandBuffer[_band2], AudioListenerPeer._audioBandBuffer[_band2]);

             */
        }
    }
}

