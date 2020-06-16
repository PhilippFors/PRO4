using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamagePlate : MonoBehaviour
{

    private Material _material;
    public bool onKick;

    public bool deathWall;

    Color _color;
    float H, S, V;

   
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
       // _color = _material.GetColor("_EmissiveColor");

       

        Color.RGBToHSV((_color), out H, out S, out V);

        if (onKick)
        {
           // EventSystem.instance.Kick += changePlateEmission;
        }

        if (deathWall)
        {
            EventSystem.instance.Kick += triggerDeathWall;
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
            .Append(_material.DOColor(Color.HSVToRGB(H, S, 10, true), "_EmissiveColor", 0.25f))
            .Append(_material.DOColor(Color.HSVToRGB(H, S,-5, true), "_EmissiveColor", 0.25f))
            .SetEase(Ease.Flash);

    
    }



    void triggerDeathWall()
    {




        DOTween.Sequence()

            .Append(_material.DOColor(Color.HSVToRGB(H, S, 10, true), "_EmissiveColor", 0.25f))
            .Join(transform.DOScaleY(120, 0.25f))
            .Append(_material.DOColor(Color.HSVToRGB(H, S, -5, true), "_EmissiveColor", 0.5f))
            .Join(transform.DOScaleY(1, 0.5f))
            .SetEase(Ease.Flash);




    }
    //_EmissiveIntensity
}
