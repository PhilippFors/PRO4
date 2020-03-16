using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Camera mainCam;
    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    public GameObject reticle;

    public GameObject lookSphere;
    public Vector3 currentDirection;


    Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 1.5f, 0));
    public bool gamepadEnabled = false;

    PlayerControls input;

    Vector2 move;
    Vector2 rotate;

    private void Awake()
    {
        input = new PlayerControls();
        input.Gameplay.Movement.performed += mv => move = mv.ReadValue<Vector2>();
        input.Gameplay.Movement.canceled += mv => move = Vector2.zero;
        input.Gameplay.Rotate.performed += rt => rotate = rt.ReadValue<Vector2>();
        input.Gameplay.Rotate.canceled += rt => rotate = Vector2.zero;
    }
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = false;
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {

        GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, 0));
        Look();
       Move();

    }

    void Db()
    {
        Debug.Log("Input performed");
    }
    private void OnEnable()
    {
        input.Gameplay.Enable();
    }
    
    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    public void Move()
    {
        if (gamepadEnabled)
        {

            
            moveVelocity.x = move.x * moveSpeed;
            moveVelocity.z = move.y * moveSpeed;
            moveVelocity.y = 0;


        }
       else
       {
           Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0, Input.GetAxisRaw("VerticalKey"));
           if ((Input.GetAxisRaw("HorizontalKey") == 1 || Input.GetAxisRaw("HorizontalKey") == -1) & Input.GetAxisRaw("VerticalKey") == 0)
           {
               moveVelocity = direction * (moveSpeed + 2); ;
           }
           else if ((Input.GetAxisRaw("VerticalKey") == 1 || Input.GetAxisRaw("VerticalKey") == -1) & Input.GetAxisRaw("HorizontalKey") == 0)
           {
               moveVelocity = direction * (moveSpeed + 2);
           }
           else
           {
               moveVelocity = direction * moveSpeed;
           }
       }
        Vector3 horizMovement = right * moveVelocity.x * Time.deltaTime;
        Vector3 vertikMovement = forward * moveVelocity.z * Time.deltaTime;
        Vector3 h = right * moveVelocity.x;
        Vector3 v = forward * moveVelocity.z;
        currentDirection = h + v;
        transform.position += horizMovement;
        transform.position += vertikMovement;

    }

    public void Look()
    {

        if (gamepadEnabled)
        {
            Vector3 input = new Vector3(rotate.x, 0, rotate.y);
            var lookRot = mainCam.transform.TransformDirection(input);
            lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);

            
            
            if (lookRot != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(lookRot);
                transform.rotation = newRotation;
              //Vector3 horizMovement = right * rotate.x * Time.deltaTime * moveSpeed;
              //Vector3 vertikMovement = forward * rotate.y * Time.deltaTime * moveSpeed;
              //
              //
              //lookSphere.transform.position += horizMovement;
              //lookSphere.transform.position += vertikMovement;
            }


        }
       else
       {
           Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
       
           float rayLength;
       
           if (groundPlane.Raycast(cameraRay, out rayLength))
           {
               pointToLook = cameraRay.GetPoint(rayLength);
               Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
               transform.LookAt(new Vector4(pointToLook.x, transform.position.y, pointToLook.z));
               lookSphere.transform.position = new Vector3(pointToLook.x, pointToLook.y, pointToLook.z);
       
       
           }
       }

    }


}
