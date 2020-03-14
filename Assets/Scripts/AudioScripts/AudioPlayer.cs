using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public int _band1;
    public int _band2;
    public bool _useBuffer;
    Material[] _material;

    Color color1;
    float angleOffset;
    Color color2;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterials;
        color1 = _material[0].GetColor("fresnelColor");
        angleOffset = _material[0].GetFloat("_voronoiAngleOffset");
        //color2 = _material[2].GetColor("_EmissionColor");
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
            angleOffset += V * 0.05f;
            //Color _color1 = new Color(AudioPeer._audioBandBuffer[_band1], AudioPeer._audioBandBuffer[_band1], AudioPeer._audioBandBuffer[_band1]);
            _material[0].SetColor("fresnelColor", Color.HSVToRGB(H, S, V, true));
            _material[0].SetFloat("_voronoiAngleOffset", angleOffset);


        }
    }
}
