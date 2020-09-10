using UnityEngine;

public class AreaBarrier : MonoBehaviour
{
    public int AreaID;
    
    public void Deactivate(){
        gameObject.SetActive(false);
    }

    public void UdpateNames(){
        AreaBarrier[] bs = FindObjectsOfType<AreaBarrier>();

        foreach(AreaBarrier a in bs){
            a.name = "Barrier_" + a.AreaID;
        }


    }
}
