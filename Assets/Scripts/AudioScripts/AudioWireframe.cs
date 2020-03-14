using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWireframe : MonoBehaviour
{
    public int _band1;
    public bool _useBuffer;
    Material[] _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials;
        //float wireThick = _material[0].GetFloat("_WireThickness");

    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {

            //float test = (AudioPeer._audioBandBuffer[_band2] * _maxScale) + _startScale;
            // Debug.Log(test, test);
            //transform.localScale = new Vector3(transform.localScale.x, AudioPeer._audioBand[_band2], transform.localScale.z);


            float V;
           
            V = AudioListenerPeer._audioBandBuffer[_band1] * 700;

            //Color _color1 = new Color(AudioPeer._audioBandBuffer[_band1], AudioPeer._audioBandBuffer[_band1], AudioPeer._audioBandBuffer[_band1]);
            _material[0].SetFloat("_WireThickness", V);


        }
    }
}
