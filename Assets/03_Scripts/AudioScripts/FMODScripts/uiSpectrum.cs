using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSpectrum : MonoBehaviour
{
   //public GameObject spectrumBars;
   Transform[] transformObj;

    public int _fqBand;
    private float scale = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = new Vector3(transform.localScale.x, (FMODAudioPeer._instance.getFqBandBuffer32(_fqBand)*scale) + 0.01f , transform.localScale.z);
        
    }
}
