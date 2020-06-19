using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_damagePlate : MonoBehaviour
{
    private Material _material;
    private float defaultLength;


    public bool onKick;
    public bool onBass;

    public bool intervalBeat;
    public int intervalCounter = 0;

    Color _color;
    float H, S, V;


    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _color = _material.GetColor("EmissionRedColor");

        Color.RGBToHSV((_color), out H, out S, out V);

        if (onKick)
        {
            EventSystem.instance.Kick += changePlateEmission;
        }

        if (onBass) 
        {
            EventSystem.instance.Bass +=  changePlateEmission;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }



    void changePlateEmission()
    {
        if (intervalBeat)
        {
            intervalCounter++;
        }
        if (intervalCounter % 2 == 0)
        {
            Debug.Log("changPlateEmission");
            DOTween.Sequence()
                .Append(_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", 0.25f))
                .Append(_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionRedColor", 0.25f))
                .SetEase(Ease.Flash);
        }
    }
}

