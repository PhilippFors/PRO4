using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLight : MonoBehaviour
{
    private FMODAudioPeer _audioPeer;
    public int _band;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    public bool _highIntensityLight;
    Light _light;
    int _multiply;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        GameObject gameObj = GameObject.Find("FMODAudioPeer");
        _audioPeer = gameObj.GetComponent<FMODAudioPeer>();
    }

    // Update is called once per frame
    void Update()
    {


        //  transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audioBandBuffer[_band] * _maxScale) + _startScale, transform.localScale.z);
        if (_highIntensityLight)
        {
            _multiply = 10000;
        }
        else
        {
            _multiply = 1;
        }


        if (_useBuffer)
        {

            _light.intensity = FMODAudioPeer._audioBandBuffer8[_band] * _multiply;
        }
       else
        {
            _light.intensity = FMODAudioPeer._audioBand8[_band] * _multiply;
        }


    }

}


