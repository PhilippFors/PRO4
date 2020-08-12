using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_damagePlate : MonoBehaviour
{
    private Material m_material;
    private float defaultLength;


    public bool m_onKick;
    public bool m_onBass;

    public bool m_intervalBeat;
    public int m_intervalCounter = 0;

    Color m_color;
    float H, S, V;


    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<MeshRenderer>().material;
        m_color = m_material.GetColor("EmissionRedColor");

        Color.RGBToHSV((m_color), out H, out S, out V);

        if (m_onKick)
        {
            EventSystem.instance.Kick += changePlateEmission;
        }

        if (m_onBass) 
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
        if (m_intervalBeat)
        {
            m_intervalCounter++;
        }
        if (m_intervalCounter % 2 == 0)
        {
            //Debug.Log("changPlateEmission");
            DOTween.Sequence()
                .Append(m_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", 0.25f))
                .Append(m_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionRedColor", 0.25f))
                .SetEase(Ease.Flash);
        }
    }
}

