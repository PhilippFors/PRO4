using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityScript.Lang;


public class PlayerAttack : MonoBehaviour
{
    PlayerControls input;

    [SerializeField] public List<Skills> skills = new List<Skills>();

    private GameObject _child; //the weapon object
    public int comboCounter = 0;

    public GameObject grenadePrefab;

    public GameObject targetPrefab;

    public GameObject target;
    
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    
    

    [SerializeField] private float moveSpeed = 5f;
    //  public Vector3 currentDirection;


   
    public GameObject AudioPeer;
    public static FMODUnity.StudioEventEmitter emitter;

   


    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    private void Awake()
    {
        input = new PlayerControls();

        //input.Gameplay.LeftAttack.performed += rt => Attack(0);
        //input.Gameplay.RightAttack.performed += rt => Attack(1);
        input.Gameplay.GrenadeThrow.performed += rt => AimMove();
        input.Gameplay.GrenadeReleaser.performed += rt => GrenadeThrow();
        input.Gameplay.Skill1.performed += rt => Skill(0);
        input.Gameplay.Skill2.performed += rt => Skill(1);
        input.Gameplay.Skill3.performed += rt => Skill(2);
        
    }

    private void Reset()
    {
    }

    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
        //EventSystem.instance.AimGrenade += AimMove;
        

        emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
        foreach (Skills skill in skills)
        {
            skill.current = 0;
        }
        
      
    }

    public Skills SkillInit(string name, float current, float max, float timer)
    {
        Skills skillTemp = ScriptableObject.CreateInstance<Skills>();
        skillTemp.skillName = name;
        skillTemp.current = current;
        skillTemp.max = max;
        skillTemp.timer = timer;

        return skillTemp;
    }

    void Skill(int id)
    {
        Skills temp = skills[id];
        if (!temp.isActive && temp.current == temp.max)
        {
            StartCoroutine(Timer(temp, id));
        }
        else
        {
            temp.isActive = false;
            emitter.SetParameter(temp.skillName, temp.deactiveValue);
        }
    }

    IEnumerator Timer(Skills temp, int id)
    {
        temp.isActive = true;
        emitter.SetParameter(temp.skillName, temp.activeValue);
        temp.current = 0;
        EventSystem.instance.OnSkill(MultiplierName.defense, temp.increaseMultValue);
        EventSystem.instance.OnSkill(MultiplierName.speed, temp.decreaseMultValue);

        yield return new WaitForSeconds(temp.timer);

        EventSystem.instance.OnSkill();
        temp.isActive = false;
        Debug.Log("hi");
        emitter.SetParameter(temp.skillName, temp.deactiveValue);
        yield return null;
    }


    void Update()
    {
        
    }

    public void GrenadeThrow()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject grenade = Instantiate(grenadePrefab, pos, transform.rotation);
        StartCoroutine(SimulateProjectile(grenade));
        EventSystem.instance.OnSetState(PlayerMovmentSate.standard);
        
    }

    public void AimMove()
    {
        EventSystem.instance.OnSetState(PlayerMovmentSate.grenade);
        target = Instantiate(targetPrefab, new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
    }
    
    IEnumerator SimulateProjectile(GameObject grenade)
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);
       
        // Move projectile to the position of throwing object + add some offset if needed.
        //grenade.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);
       
        // Calculate distance to target
        float target_Distance = Vector3.Distance(grenade.transform.position, target.transform.position);
 
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
 
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
   
        // Rotate projectile to face the target.
        grenade.transform.rotation = Quaternion.LookRotation(target.transform.position - grenade.transform.position);
       
        float elapseTime = 0;
 
        while (elapseTime < flightDuration)
        {
            if (grenade == null)
            {
                break;
            }
            grenade.transform.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);
           
            elapseTime += Time.deltaTime;
 
            yield return null;
        }

        if (elapseTime >= flightDuration || grenade == null)
        {
            Destroy(target);
            EventSystem.instance.OnExplode();
            yield return null;
        }
    }   

    public void Attack(int attack)
    {
        if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Wait"))
        {
           
            EventSystem.instance.OnSetState(PlayerMovmentSate.attack);
            switch (attack)
            {
                case 0:
                    _child.GetComponent<Animator>().SetTrigger("FastAttack");
                    break;
                case 1:
                    _child.GetComponent<Animator>().SetTrigger("SlowAttack");
                    break;
            }
            
        }
    }
}
