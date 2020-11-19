using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class CameraController : MonoBehaviour
{
    public Transform player;
    public Camera mainCam;
    private PlayerStateMachine playercontrols;
    public float cameraFollowSpeed = 5f;
    public float targetBias = 0.5f;
    public float gamepadDist = 5f;
    bool mouseused;
    bool gamepadused;
    Plane groundPlane;
    Vector3 s;

    [SerializeField] LevelManager l;
    private void OnEnable()
    {
        GameManager.instance.initAll += SetPos;
    }
    private void OnDisable()
    {
        GameManager.instance.initAll -= SetPos;
    }

    private void Start()
    {
        playercontrols = player.gameObject.GetComponent<PlayerStateMachine>();
    }

    void SetPos()
    {
        StartCoroutine(WaitForStart());
    }

    private void Update()
    {
        GetCamPosition();
        GamePadCam();
        s.y = player.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, s, Time.deltaTime * cameraFollowSpeed);
    }

    void GetCamPosition()
    {
        if (playercontrols.input.Gameplay.Look.triggered || mouseused)
        {
            gamepadused = false;
            mouseused = true;
            groundPlane = new Plane(Vector3.up, new Vector3(0, player.position.y, 0));
            //creating the Ray
            Ray cameraRay = mainCam.ScreenPointToRay(playercontrols.mouseLook);

            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 rayPoint = cameraRay.GetPoint(rayLength);

                Vector3 dist = rayPoint - player.position;
                s = player.position + dist * targetBias;
            }
            else
            {

                s = player.position;
            }

        }
    }

    void GamePadCam()
    {
        if (playercontrols.input.Gameplay.Rotate.triggered || gamepadused)
        {

            gamepadused = true;
            mouseused = false;
            Vector2 value = playercontrols.input.Gameplay.Rotate.ReadValue<Vector2>();
            Vector3 direction = new Vector3(value.x, 0, value.y);

            Vector3 horizMovement = playercontrols.right * direction.x;
            Vector3 vertikMovement = playercontrols.forward * direction.z;

            Vector3 moveD = horizMovement + vertikMovement;
            Vector3 f = player.position + moveD * gamepadDist;
            Vector3 dist = f - player.position;
            s = player.position + dist * targetBias;
        }
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForEndOfFrame();
        transform.position = l.playerSpawn.position;
    }
}
