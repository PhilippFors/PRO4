using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLaser : MonoBehaviour
{
    private IEnumerator coroutine;
    public int _fqBand;
    public float _scale;
    public float _threshold;

    Vector3 startScale = new Vector3(3f, 3f, 3f);
    Vector3 newScale = new Vector3(3f, 10f, 3f);
    float speed = 0.5f;

   

    // Start is called before the first frame update
    void Start()
    {
        coroutine = MoveToSpot();
      
        //EventSystem.instance.Bass += shootLaser;

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void shootLaser()
    {
       // StartCoroutine(coroutine);
    }

    IEnumerator MoveToSpot()
    {
        Vector3 Gotoposition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        float elapsedTime = 0;
        float waitTime = 3f;
        Vector3 currentPos = transform.position;

        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        }
        return null;
    }


}
