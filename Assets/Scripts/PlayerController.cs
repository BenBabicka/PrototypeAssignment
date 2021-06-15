using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public InputMaster controlls;
    public GameObject playersCamera;
    public Transform groundCheck;
    public LayerMask ground;
    public float walkSpeed = 120f;
    public float sprintSpeed = 200f;
    public float jumpForce;
    [Header("Console")]
    public float rotationDamping = 20f;
    public float rotationSpeed = 10f;

    [Header("PC")]

    public float mouseSensitivityY = 0.8f;
    public float mouseSensitivityX = 0.8f;
    public float clamp = 10;
    Vector2 move;
    Vector2 controllerRoation;
    Vector2 mouseRoation;


    bool sprinting;
    bool pc;
    bool controller;
    float moveSpeed;
    float rotationX;
    float rotX;

    Vector3 orginalCameraPosition;

    bool jump;
    bool canJump;
    Transform wall;
    private void Awake()
    {
        controlls = new InputMaster();

        controlls.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controlls.Player.Movement.canceled += ctx => move = Vector2.zero;

        controlls.Player.ControllerLook.performed += ctx => controllerRoation = ctx.ReadValue<Vector2>();
        controlls.Player.ControllerLook.canceled += ctx => controllerRoation = Vector2.zero;
        
        controlls.Player.ControllerLook.performed += ctx => controller = true;
        controlls.Player.ControllerLook.canceled += ctx => controller = false;
  

        controlls.Player.MouseLook.performed += ctx => mouseRoation.y = ctx.ReadValue<Vector2>().y * mouseSensitivityY;
        controlls.Player.MouseLook.performed += ctx => mouseRoation.x = ctx.ReadValue<Vector2>().x * mouseSensitivityX;
        controlls.Player.MouseLook.canceled += ctx => mouseRoation = Vector2.zero;

        controlls.Player.MouseLook.performed += ctx => pc = true;
        controlls.Player.MouseLook.canceled += ctx => pc = false;


        controlls.Player.Sprint.performed += ctx => sprinting = true;
        controlls.Player.Sprint.canceled += ctx => sprinting = false;

        controlls.Player.Jump.performed += ctx => jump = true;
        controlls.Player.Jump.canceled += ctx => jump = false;

    }

    private void OnEnable()
    {
        controlls.Enable();
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
   
        moveSpeed = walkSpeed;
        orginalCameraPosition = playersCamera.transform.localPosition;
    }
    void FixedUpdate()
    {
        HideWalls();

        if (Mathf.Abs(controllerRoation.x) < 0.125f)
        {
            controllerRoation.x = 0;
        }
        if (Mathf.Abs( controllerRoation.y )< 0.125f)
        {
            controllerRoation.y = 0;
        }
       
        RaycastHit hit;
        if(Physics.Raycast(groundCheck.transform.position, -groundCheck.up, out hit,.1f, ground))
        {
            canJump = true;
        }
        else
        {
            canJump = false;

        }

        if (jump && canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        //Players Rotation on a controller
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y + controllerRoation.x * rotationSpeed, 0f), Time.fixedDeltaTime * rotationDamping);


        if (controller)
        {
            rotX = playersCamera.transform.localRotation.eulerAngles.x + -controllerRoation.y * rotationSpeed;

            if (playersCamera.transform.localRotation.eulerAngles.x > 360 - clamp)
            {
                rotX = Mathf.Clamp(rotX, 360.25f - clamp, 370);
            }
            else
            {
                rotX = Mathf.Clamp(rotX, -clamp, clamp);
            }

            playersCamera.transform.localRotation = Quaternion.Slerp(playersCamera.transform.localRotation, Quaternion.Euler(rotX, 0, 0), Time.deltaTime * rotationDamping);
        }
        
        //Players movement that is relative to the direction the player is facing
        rb.AddRelativeForce(new Vector3(move.x, 0, move.y) * moveSpeed, ForceMode.Force);
    }
    private void Update()
    {
 
        if (sprinting)
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        //Players rotation on a mouse (needs to be in update because fixed update has jerky movement)
        if (pc)
        {
            transform.Rotate(Vector3.up, (mouseRoation.x * mouseSensitivityY));

            rotationX -= mouseRoation.y * mouseSensitivityY;
            rotationX = Mathf.Clamp(rotationX, -clamp, clamp);

            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = rotationX;
            playersCamera.transform.eulerAngles = targetRotation;
        }
    }

    void HideWalls()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playersCamera.transform.position - transform.position, out hit, Mathf.Abs(orginalCameraPosition.z)))
        {
            Vector3 hitPosition = transform.InverseTransformPoint(hit.point);

            if(wall)
            {
                if(wall != hit.transform)
                {
                    if (wall.GetComponent<MeshRenderer>())
                    {
                        wall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    }
                }
            }

            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "playerProjectile" && hit.collider.gameObject.tag !="Building" &&hit.collider.gameObject.tag != "Enemy")
            {
                wall = hit.transform;
                if (wall.GetComponent<MeshRenderer>())
                {
                    wall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
                playersCamera.transform.localPosition = new Vector3(orginalCameraPosition.x, orginalCameraPosition.y, hitPosition.z);
                
            } 
        }
        else
        {
            if (wall)
            {
                if (wall.GetComponent<MeshRenderer>())
                {
                    wall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
                wall = null;
            }
            playersCamera.transform.localPosition = orginalCameraPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, playersCamera.transform.position - transform.position);
    }


}
