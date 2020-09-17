using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AR_mover : MusicAnalyzer
{
    public float m_moveX = 0;
    public float m_moveY = 0;
    public float m_moveZ = 0;

    public bool m_backAndForth = true;


    // Start is called before the first frame update
    void Start()
    {
        addActionToEvent();
    }

    protected override void objectAction()
    {
        increaseIntervalCounter();
        // moveSimple();

        if (m_moveX > 0)
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
        
    }

    protected void increaseIntervalCounter()
    {

        if (m_intervalBeat)
        {
        
                    m_intervalCounter++;
         }



            // m_overallIntervalCounter++;
        }


    }
