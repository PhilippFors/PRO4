using UnityEngine;

public class AreaBarrier : MonoBehaviour
{
    public int AreaID;
    public bool active;

    BoxCollider col => GetComponent<BoxCollider>();
    MeshRenderer rend => GetComponent<MeshRenderer>();
    public void Deactivate()
    {
        col.enabled = !active;
        rend.enabled = !active;
        active = !active;
    }

    public void Activate()
    {
        col.enabled = !active;
        rend.enabled = !active;
        active = !active;
    }

    public void UdpateNames()
    {
        AreaBarrier[] bs = FindObjectsOfType<AreaBarrier>();

        foreach (AreaBarrier a in bs)
        {
            a.name = "Barrier_" + a.AreaID;
        }


    }
}
