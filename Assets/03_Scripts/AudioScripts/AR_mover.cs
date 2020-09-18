﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_mover : MusicAnalyzer
{
    public float m_moveX = 0;
    public float m_moveY = 0;
    public float m_moveZ = 0;

    public bool m_backAndForth = true;

    private Material m_material;
    float H, S, V;

    float x;

    public bool m_activateComponent = false;

    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<MeshRenderer>().material;
        
    }

    protected override void objectAction()
    {
        increaseIntervalCounter();
        // moveSimple();

        if (m_moveX > 0 && !colorErrorActive)
        {
            if (checkInterval())
            {
               
            }
            if (m_intervalBeat && checkInterval())
            {
                transform.DOLocalMoveX(transform.localPosition.x + m_moveX, m_actionInDuration);
            }
            if (m_backAndForth && !checkInterval())
            {
                transform.DOLocalMoveX(transform.localPosition.x - m_moveX, m_actionInDuration);
                
            }
        }
        if (m_moveZ > 0)
        {
            if (m_intervalBeat && m_intervalCounter % 2 == 0)
            {
                transform.DOLocalMoveZ(transform.position.z + m_moveZ, m_actionInDuration);
            }
            else if (m_backAndForth && (m_intervalCounter % 2 != 0))
            {
                transform.DOLocalMoveZ(transform.position.z - m_moveZ, m_actionInDuration);
            }
        }
        if (m_moveY > 0)
        {
            if (m_intervalBeat && m_intervalCounter % 2 == 0)
            {
                transform.DOLocalMoveY(transform.position.y + m_moveY, m_actionInDuration);
              
            }
            else if (m_backAndForth && (m_intervalCounter % 2 != 0))
            {
                transform.DOLocalMoveY(transform.position.y - m_moveY, m_actionInDuration);
            }
        }

       

    }

    void moveSimple()
    {

       
    }



    // Update is called once per frame
    void Update()
    {

        if (colorErrorActive && addedToEvent)
        {
            removeActionFromEvent();
        }
        else if (!colorErrorActive && !addedToEvent && m_activateComponent)
        {
            addActionToEvent();
        }
        


        if (colorErrorActive)
        {
           

            if (m_onKick)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
            }
            else if (m_onHighHat)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
            }
            else if (m_onSnare)
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
            }

            m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            /*
            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
            */

        }
        else
        {
            if (m_onKick)
            {
                Color.RGBToHSV((m_blueChannelActiveColor), out H, out S, out V);
            }
            else if (m_onHighHat)
            {
                Color.RGBToHSV((m_redChannelActiveColor), out H, out S, out V);
            }
            else if (m_onSnare)
            {
                Color.RGBToHSV((m_bothChannelActiveColor), out H, out S, out V);
            }

            m_material.SetColor("EmissionBlueColor", Color.HSVToRGB(H, S, V));
            /*
            if (!m_holdHelper)
            {
                m_material.SetColor("EmissionRedColor", Color.HSVToRGB(H, S, 10));
            }
            */
        }
    }

    protected void increaseIntervalCounter()
    {

        if (m_intervalBeat)
        {
        
                    m_intervalCounter++;
         }



            // m_overallIntervalCounter++;
    }


    public void activateComponent()
    {
        m_activateComponent = true;
            addActionToEvent();
    }

    public void deactivateComponent()
    {
        m_activateComponent = false;
        removeActionFromEvent();
    }

}


