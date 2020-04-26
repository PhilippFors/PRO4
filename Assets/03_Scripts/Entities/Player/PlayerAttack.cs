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
    public float comboCounter = 0;

    public GameObject grenadePrefab;

    public GameObject grenadeHitLoc;

    public GameObject hitLoc;
    
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    

    [SerializeField] private float moveSpeed = 5f;
    //  public Vector3 currentDirection;


    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Vector2 hitLocMove = Vector3.zero;

    public GameObject AudioPeer;
    public static FMODUnity.StudioEventEmitter emitter;

    private bool hold;


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

        input.Gameplay.LeftAttack.performed += rt => LeftAttack();
        input.Gameplay.RightAttack.performed += rt => RightAttack();
        input.Gameplay.GrenadeThrow.performed += rt => AimMove();
        input.Gameplay.GrenadeReleaser.performed += rt => GrenadeThrow();
        input.Gameplay.Skill1.performed += rt => Skill(0);
        input.Gameplay.Skill2.performed += rt => Skill(1);
        input.Gameplay.Skill3.performed += rt => Skill(2);
        
        input.Gameplay.GrenadeAim.performed += ctx => hitLocMove = ctx.ReadValue<Vector2>();
        input.Gameplay.GrenadeAim.canceled += ctx => hitLocMove = Vector2.zero;
    }

    private void Reset()
    {
    }

    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
        EventSystem.instance.AimGrenade += AimMove;
        

        emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
        foreach (Skills skill in skills)
        {
            skill.current = 0;
        }
        
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
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
        
        if (hold)
        {
           
            hitLocMove = input.Gameplay.GrenadeAim.ReadValue<Vector2>();
            Vector3 direction = new Vector3(hitLocMove.x, 0, hitLocMove.y);
            moveVelocity = direction * moveSpeed * Time.deltaTime;
            moveVelocity.y = 0;
            Vector3 horizMovement = right * moveVelocity.x;
            Vector3 vertikMovement = forward * moveVelocity.z;
            hitLoc.transform.position += horizMovement;
            hitLoc.transform.position += vertikMovement;
        }
    }

    public void GrenadeThrow()
    {
        hold = false;
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        StartCoroutine(SimulateProjectile(grenade));
      
        
    }

    public void AimMove()
    {
        hold = true;
        hitLoc = Instantiate(grenadeHitLoc, new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
    }
    
    IEnumerator SimulateProjectile(GameObject grenade)
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);
       
        // Move projectile to the position of throwing object + add some offset if needed.
        //grenade.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);
       
        // Calculate distance to target
        float target_Distance = Vector3.Distance(grenade.transform.position, hitLoc.transform.position);
 
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
 
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
   
        // Rotate projectile to face the target.
        grenade.transform.rotation = Quaternion.LookRotation(hitLoc.transform.position - grenade.transform.position);
       
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
            Destroy(hitLoc);
            EventSystem.instance.OnExplode();
            yield return null;
        }
    }   

    public void LeftAttack()
    {
        if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Wait"))
        {
            {
                _child.GetComponent<Animator>().SetTrigger("FastAttack");
            }
        }
    }

    public void RightAttack()
    {
        if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Wait"))
        {
            {
                _child.GetComponent<Animator>().SetTrigger("SlowAttack");
            }
        }
    }
}
