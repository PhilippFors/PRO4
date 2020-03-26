using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBullet : MonoBehaviour
{
    public int _band1;
    public bool _useBuffer;
    Material[] _material;

    Color color1;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials;
        color1 = _material[0].GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {

            //float test = (AudioPeer._audioBandBuffer[_band2] * _maxScale) + _startScale;
            // Debug.Log(test, test);
            //transform.localScale = new Vector3(transform.localScale.x, AudioPeer._audioBand[_band2], transform.localScale.z);


            float H, S, V;
            Color.RGBToHSV((color1), out H, out S, out V);
            V = AudioListenerPeer._audioBandBuffer[_band1];

          
            _material[0].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));



        }

        else
        {
            // transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audioBand[_band] * _maxScale) + _startScale, transform.localScale.z);

            float H, S, V;
            Color.RGBToHSV((color1), out H, out S, out V);
            V = AudioListenerPeer._audioBand[_band1];

           
            _material[0].SetColor("_EmissionColor", Color.HSVToRGB(H, S, V, true));
        }
    }
}
