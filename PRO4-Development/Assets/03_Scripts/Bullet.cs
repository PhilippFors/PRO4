using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetValue(Vector3 d, float s)
    {
        direction = d;
        speed = s;
        Destroy(this.gameObject, 5f);
    }


}
