using UnityEngine;

public class AreaBarrier : MonoBehaviour
{
    public int AreaID;
    
    public void Deactivate(){
        gameObject.SetActive(false);
    }
}
