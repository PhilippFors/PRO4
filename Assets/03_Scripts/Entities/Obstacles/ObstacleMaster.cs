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
    bool masterActive = true;
    public float masterRecoveryTime = 3f;

    void Update()
    {
        if (body.health <= 0)
        {
            sender.active = false;
            nextReciever.active = false;
            body.OnDeath();
            StartCoroutine(WallRecover());
        }

        if (!sender.active & !selfReciever.active)
        {
            DeactivateMaster();
            StartCoroutine(MasterRecover());
        }
    }
    void DeactivateMaster()
    {
        masterActive = false;
        //TODO: Animate Pillar deactivation
        //TODO: Deactivate collider

    }
    void ActivateMaster()
    {
        masterActive = true;
        //TODO: Animate Pillar activation
        //TODO: Activate collider
    }
    IEnumerator MasterRecover()
    {
        yield return new WaitForSeconds(masterRecoveryTime);
        ActivateMaster();
    }
    IEnumerator WallRecover()
    {
        while (true)
        {
            if (body.health >= body.minActivationHealth)
            {
                body.Activate();
                break;
            }
            yield return null;
        }

        while (!masterActive)
        {
            yield return null;
        }
        sender.active = true;
        nextReciever.active = true;
    }
}
