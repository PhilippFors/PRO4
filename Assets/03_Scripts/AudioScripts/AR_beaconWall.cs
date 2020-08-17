using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_beaconWall : MusicAnalyzer
{

    private Material _material;
    private float defaultLength;


   
    public float lengthOfLaser;


    Color _beaconEmissiveColor;
    float H, S, V;

    GameObject _energyWall;
    Color _energyWallColor;
    Material _energyWallMaterial;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        _beaconEmissiveColor = _material.GetColor("EmissionRedColor");

        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        _energyWallMaterial = _energyWall.GetComponent<MeshRenderer>().material;
        _energyWallColor = _energyWallMaterial.GetColor("EmissionRedColor");

        defaultLength = _energyWall.transform.localScale.y;


        Color.RGBToHSV((_beaconEmissiveColor), out H, out S, out V);

        addActionToEvent();

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void objectAction()
    {
        increaseIntervalCounter();
        if (checkInterval())
        {
            DOTween.Sequence()
           .Append(_energyWallMaterial.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", 0.25f))
           .Join(_energyWall.transform.DOScaleY(lengthOfLaser, 0.25f))
           .Append(_energyWallMaterial.DOColor(Color.HSVToRGB(H, S, 1, true), "EmissionRedColor", 0.5f))
           .Join(_energyWall.transform.DOScaleY(defaultLength, 0.5f))
           .SetEase(Ease.Flash);
        }

    }
}
