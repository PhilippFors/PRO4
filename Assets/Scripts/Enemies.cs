using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletEmitter;
    
    public bool playerFound = false;
    private GameObject player;
    public GameObject Bullet;
    public GameObject detector;
    private bool shot;

  
    public float Bullet_Forward_Force;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        LookAtPlayer();
        Rotate();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down);
    }

    void LookAtPlayer()
    {
        if (playerFound)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
    }

    void Shooting()
    {
        if (playerFound)
        {
            if (!shot)
            {
                shot = true;
                StartCoroutine("ShootTimer");
            }
        }
    }
    void FindPlayer()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(detector.transform.position, detector.transform.forward, out hit))
        {
            GameObject obj = hit.transform.gameObject;
            if (obj.name.Equals("Player"))
            {

                Shooting();
                playerFound = true;
                player = obj;
                
            }
            else
            {
                playerFound = false;
                shot = false;
            }
        }
    }

    void EnemyShoot()
    {
        //The Bullet instantiation happens here.
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Temporary_Bullet_Handler.GetComponent<Bullet>().SetValue(transform.forward, Bullet_Forward_Force);
        
    }
    IEnumerator ShootTimer()
    {
        while (playerFound)
        {
            EnemyShoot();
            
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }
    void Rotate()
    {
        if(!playerFound)
        {
            transform.eulerAngles += new Vector3(0, 50f, 0) * Time.deltaTime;
        }
        
    }

}
