using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Light : MonoBehaviour
{
    public int _audioBand;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    public bool _highIntensityLight;
    Light _light;
    int _multiply;

    bool _enableListener;
    bool _bassListener;
    bool _kickListener;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();

        if (_highIntensityLight)
        {
            _multiply = 10000;
        }
        else
        {
            _multiply = 1;

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_useBuffer)
        {
            _light.intensity = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand) * _multiply;
        }
       else
        {
            _light.intensity = FMODAudioPeer._instance.getFqBandBuffer8(_audioBand) * _multiply;
        }


    }

}


