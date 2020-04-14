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

    [SerializeField] private float moveSpeed = 5f;
    //  public Vector3 currentDirection;


    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Vector2 move = Vector3.zero;

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

        input.Gameplay.LeftAttack.performed += rt => LeftAttack();
        input.Gameplay.RightAttack.performed += rt => RightAttack();
        //input.Gameplay.GrenadeThrow.performed += rt => EventSystem.instance.OnGrenadeAim();
        input.Gameplay.Skill1.performed += rt => Skill(0);

        
    }

    private void Reset()
    {
        
    }

    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player

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
            
            StartCoroutine(Timer(temp));
        }
        else
        {
            temp.isActive = false;
            emitter.SetParameter(temp.skillName, temp.deactiveValue);
        }
    }

    IEnumerator Timer(Skills temp)
    {
        temp.isActive = true;
        emitter.SetParameter(temp.skillName, temp.activeValue);
        temp.current = 0;
        EventSystem.instance.OnSkill(MultiplierName.defense, temp.increaseMultValue, temp.timer);
        EventSystem.instance.OnSkill(MultiplierName.damage, temp.decreaseMultValue, temp.timer);
        
        yield return new WaitForSeconds(temp.timer);
        
        temp.isActive = false;
        emitter.SetParameter(temp.skillName, temp.deactiveValue);
        yield return null;
    }


    void Update()
    {
    }

    public void GrenadeThrow()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        
    }

    public void AimMove()
    {
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
