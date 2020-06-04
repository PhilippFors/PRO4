using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaster : MonoBehaviour
{
    // Start is called before the first frame update
    Reciever nextReciever;
    Reciever selfReciever;
    Sender sender;
    ObstacleBody body;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (body.health <= 0)
        {
            sender.active = false;

        }
    }

    IEnumerator Recovery()
    {
        yield return null;
    }
}
