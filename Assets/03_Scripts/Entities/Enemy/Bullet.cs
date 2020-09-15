using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed;
    public float damage;
    public Rigidbody rb => GetComponent<Rigidbody>();

    // Update is called once per frame
    private void Update()
    {
        //transform.position += direction * speed * Time.deltaTime;
    }

    public void InitBUllet(Vector3 dir, float s, float d)
    {
        direction = dir;
        speed = s;
        damage = d;
        rb.AddForce(dir * s, ForceMode.Impulse);
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBody p = other.GetComponent<PlayerBody>();
        if(p != null)
        {
            EventSystem.instance.OnAttack(p, damage);
        }
        Destroy(this.gameObject);
    }


}
