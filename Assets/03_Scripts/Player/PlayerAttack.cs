using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

public class Skills:ScriptableObject
{
    public float current;
    public float max;
    public float timer;
}


public class PlayerAttack : MonoBehaviour
{
    PlayerControls input;
    
    public Skills[] skill = new Skills[4];

    private GameObject _child; //the weapon object
    public float comboCounter = 0;
    
    public GameObject grenadePrefab;
    
    public GameObject grenadeHitLoc;
    
    [SerializeField] private float moveSpeed = 5f;
  //  public Vector3 currentDirection;


    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Vector2 move = Vector3.zero;
    
    public GameObject AudioPeer;
    FMODUnity.StudioEventEmitter emitter;

    bool lowPassActive;
    bool highPassActive;
    bool active;

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
        input.Gameplay.GrenadeThrow.performed += rt => GrenadeThrow();
        input.Gameplay.GrenadeThrow.performed += rt => GrenadeThrow();
        input.Gameplay.Skill1.performed += rt => LowPassSkill();
        input.Gameplay.Skill2.performed += rt => HighPassSkill();

        skill[0] = SkillInit(0, 6, 5f);
        skill[1] = SkillInit(0, 4, 5f);
        skill[2] = SkillInit(0, 2, 5f);


    }

    Skills SkillInit(float current, float max, float timer)
    {
        Skills skillTemp = ScriptableObject.CreateInstance<Skills>();
        skillTemp.current = current;
        skillTemp.max = max;
        skillTemp.timer = timer;
        return skillTemp;
    }
    
    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player
        lowPassActive = false;
        highPassActive = false;
        emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
        
        
    }

    void LowPassSkill()
    {
        if (!lowPassActive && skill[0].current == skill[0].max)
        {
            
            StartCoroutine(nameof(LowPassSkillTimer));
        }
        else
        {
            lowPassActive = false;
            emitter.SetParameter("LowPass", 1f);
        }
    }

    IEnumerator LowPassSkillTimer()
    {
        lowPassActive = true; 
        emitter.SetParameter("LowPass", 0.3f);
        yield return new WaitForSeconds(skill[0].timer);
        lowPassActive = false;
        emitter.SetParameter("LowPass", 1f);
        skill[0].current = 0;
    }
    
    void HighPassSkill()
    {
        
        if (!highPassActive && skill[1].current == skill[1].max)
        {
            
            StartCoroutine(nameof(HighPassSkillTimer));
        }
        else
        {
            highPassActive = false;
            emitter.SetParameter("HighPass", 1f);
        }
    }

    
    IEnumerator HighPassSkillTimer()
    {
        highPassActive = true;
        emitter.SetParameter("HighPass", 0.5f);
        yield return new WaitForSeconds(skill[1].timer);
        highPassActive = false;
        emitter.SetParameter("HighPass", 1f);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrenadeThrow()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        GameObject hitLoc = Instantiate(grenadeHitLoc, transform.position, transform.rotation);
        
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(move.x, 0, move.y);
        moveVelocity = direction * moveSpeed * Time.deltaTime;
        moveVelocity.y = 0;
        Vector3 horizMovement = right * moveVelocity.x;
        Vector3 vertikMovement = forward * moveVelocity.z;
        hitLoc.transform.position += horizMovement;
        hitLoc.transform.position += vertikMovement;

    }
    
    public void HitMove()
    {
        
        //currentDirection = horizMovement + vertikMovement;
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