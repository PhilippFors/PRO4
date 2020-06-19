using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_beaconWall : MonoBehaviour
{

    private Material _material;
    private float defaultLength;


    public bool onKick;
    public bool onBass;

    public bool intervalBeat;
    public int intervalCounter = 0;
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

        if (onKick)
        {
            EventSystem.instance.Kick += triggerDeathWall;
        }

        if (onBass)
        {
            EventSystem.instance.Bass += triggerDeathWall;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void triggerDeathWall()
    {
        if (intervalBeat)
        {
            intervalCounter++;
        }
        if (intervalCounter % 2 == 0)
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
