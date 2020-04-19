using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLaser : MonoBehaviour
{

    public int _fqBand;
    public float _scale;
    public float _threshold;

    Vector3 startScale = new Vector3(3f, 3f, 3f);
    Vector3 newScale = new Vector3(3f, 10f, 3f);
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
        bool beatFulld2 = false;
        if (beatFulld2)
        {

            transform.localScale = newScale;
         

        }
        else
        {
            Vector3 currentScale = transform.localScale;
            transform.localScale = Vector3.Lerp(currentScale, startScale, 10 * Time.deltaTime);
        }
        //Debug.Log("FQVALUE" + fqValueHard);
    }
}
