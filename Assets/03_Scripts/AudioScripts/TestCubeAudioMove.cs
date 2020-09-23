using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCubeAudioMove : MonoBehaviour
{
    int moveCounter = 0;

    public bool eventTest;

    // Start is called before the first frame update
    void Start()
    {

        if (eventTest)
        {
            MyEventSystem.instance.Kick += test;
            MyEventSystem.instance.Kick += incMoveCounter;
        } else
        {
  
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void moveObject(int id)
    {
        switch (id)
        {
            case 0:
                transform.DOLocalMoveZ(transform.position.z + 2, 0.5f); 
                //transform.Translate(new Vector3(0, 0, 5));
                break;
            case 1:
                transform.DOLocalMoveX(transform.position.x - 2, 0.5f);
                //transform.Translate(new Vector3(-5, 0, 0));
                break;
            case 2:
                transform.DOLocalMoveZ(transform.position.z - 2, 0.5f);
                //transform.Translate(new Vector3(0, 0, -5));
                break;
            case 3:
                transform.DOLocalMoveX(transform.position.x + 2, 0.5f);
                //transform.Translate(new Vector3(5, 0, 0));
                break;
        }
    }

    void test()
    {
        moveObject(moveCounter);
    }

    void incMoveCounter()
    {
        moveCounter++;
        if (moveCounter == 4) moveCounter = 0;
    }

   
}
