using UnityEngine;

public class AreaBarrier : MonoBehaviour
{
    public int AreaID;
    public bool active = true;
    public void Deactivate(){
        gameObject.SetActive(!active);
    }

    public void Activate(){
        gameObject.SetActive(!active);
    }

    public void UdpateNames(){
        AreaBarrier[] bs = FindObjectsOfType<AreaBarrier>();

        foreach(AreaBarrier a in bs){
            a.name = "Barrier_" + a.AreaID;
        }


    }
}
