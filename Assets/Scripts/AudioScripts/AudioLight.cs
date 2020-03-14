using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLight : MonoBehaviour
{
    public int _band1;
    public int _band2;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {


        //  transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audioBandBuffer[_band] * _maxScale) + _startScale, transform.localScale.z);


        _light.intensity = AudioListenerPeer._audioBandBuffer[_band1];


    }

}


