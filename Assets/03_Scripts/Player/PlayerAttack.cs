using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public string name;
    public float current = 0;
    public float max;
    public float timer;
    public bool isActive = false;
    public float activeValue = 0.3f;
    public float deactiveValue = 1f;

    
}


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
        input.Gameplay.GrenadeThrow.performed += rt => GrenadeThrow();
        input.Gameplay.GrenadeThrow.performed += rt => GrenadeThrow();
        input.Gameplay.Skill1.performed += rt => Skill(0);

    }

    private void Reset()
    {
        
    }

    void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject; //first child object of the player

        emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
    }
    
    public Skills SkillInit(string name, float current, float max, float timer)
    {
        Skills skillTemp = ScriptableObject.CreateInstance<Skills>();
        skillTemp.name = name;
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
            emitter.SetParameter(temp.name, temp.deactiveValue);
        }
    }

    IEnumerator Timer(Skills temp)
    {
        temp.isActive = true;
        emitter.SetParameter(temp.name, temp.activeValue);
        temp.current = 0;
        
        yield return new WaitForSeconds(temp.timer);
        
        temp.isActive = false;
        emitter.SetParameter(temp.name, temp.deactiveValue);
        
    }


   


    
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