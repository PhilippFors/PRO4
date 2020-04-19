using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLaser : MonoBehaviour
{

    public int _fqBand;
    public float _scale;
    public float _threshold;

    Vector3 startScale = new Vector3(1f, 1f, 1f);
    Vector3 newScale = new Vector3(1f, 10f, 1f);
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fqValueHard = 0;
        float fqValueBuffer = 0;
        bool beatFull = FMODAudioPeer._instance.getBeatFull();
        if (beatFull)
        {
            transform.localScale = Vector3.Lerp(startScale, newScale, 1000* Time.deltaTime);
            Debug.Log("BARFULL");
        }
        else
        {
            transform.localScale = Vector3.Lerp(newScale, startScale, 1000 * Time.deltaTime);
        }
        //Debug.Log("FQVALUE" + fqValueHard);
    }
}
