using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLaser : MonoBehaviour
{

    public int _fqBand;
    public float _scale;
    public float _threshold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fqValueHard = FMODAudioPeer.getFqBand8(_fqBand);
        float fqValueBuffer = FMODAudioPeer.getFqBandBuffer8(_fqBand);
        if (fqValueHard > _threshold)
        {
            transform.localScale = new Vector3(transform.localScale.x, (fqValueBuffer * _scale) + 0.01f, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, fqValueBuffer * (_scale/(_scale/4)), transform.localScale.z);
        }
        //Debug.Log("FQVALUE" + fqValueHard);
    }
}
