using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamagePlate : MonoBehaviour
{

    private Material _material;
    private float defaultLength;


    public bool onKick;
    public bool onBass;

    public bool deathWall;
    public bool damagePlate;


    public float lengthOfLaser;


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
           // EventSystem.instance.Kick += changePlateEmission;
        }

        if (deathWall)
        {
            if (onKick)
                EventSystem.instance.Kick += triggerDeathWall;
            else if (onBass)
                EventSystem.instance.Bass += triggerDeathWall;
           
            defaultLength = transform.localScale.y;

        }

        if (damagePlate)
        {
             EventSystem.instance.Kick += changePlateEmission;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void changePlateEmission()
    {
        Debug.Log("changPlateEmission");


        DOTween.Sequence()
            .Append(_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", 0.25f))
            .Append(_material.DOColor(Color.HSVToRGB(H, S,-10, true), "EmissionRedColor", 0.25f))
            .SetEase(Ease.Flash);

    
    }



    void triggerDeathWall()
    {




        DOTween.Sequence()

            .Append(_material.DOColor(Color.HSVToRGB(H, S, 10, true), "_EmissiveColor", 0.25f))
            .Join(transform.DOScaleY(lengthOfLaser, 0.25f))
            .Append(_material.DOColor(Color.HSVToRGB(H, S, -5, true), "_EmissiveColor", 0.5f))
            .Join(transform.DOScaleY(defaultLength, 0.5f))
            .SetEase(Ease.Flash);




    }
    //_EmissiveIntensity
}
