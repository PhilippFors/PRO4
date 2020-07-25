using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Reciever nextReciever;
    public Reciever selfReciever;
    public Sender sender;
    public ObstacleBody body;
    BoxCollider col => GetComponent<BoxCollider>();
    bool masterActive = true;
    public float masterRecoveryTime = 3f;

    void Update()
    {
        if (body.health <= 0)
        {
            sender.active = false;
            if (nextReciever != null)
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
        if (masterActive)
        {
            masterActive = false;
            transform.position += new Vector3(0, -0.5f, 0);
            col.enabled = false;

        }

        //TODO: Animate Pillar deactivation
        //TODO: Deactivate collider

    }
    void ActivateMaster()
    {
        if (!masterActive)
        {
            masterActive = true;
            transform.position += new Vector3(0, 0.5f, 0);
            col.enabled = true;
            if (sender.found)
                sender.active = true;
            if (nextReciever != null)
                nextReciever.active = true;
        }

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
            if (masterActive)
                if (body.health >= body.minActivationHealth)
                {
                    body.Activate();
                    yield break;
                }
            yield return null;
        }
    }
}
