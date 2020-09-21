using UnityEngine;

public class AreaBarrier : MonoBehaviour
{
    public int AreaID;
    public bool active;

    public BoxCollider col;
    public MeshRenderer rend;
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
