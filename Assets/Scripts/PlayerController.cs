using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public InputMaster controlls;
    public GameObject playersCamera;
    public float movementSpeed = 200f;

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

    bool pc;
    bool controller;
    float rotationX;
    float controllerRotationX;
    float rotX;
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


    }

    private void OnEnable()
    {
        controlls.Enable();
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(controllerRoation.x) < 0.125f)
        {
            controllerRoation.x = 0;
        }
        if (Mathf.Abs( controllerRoation.y )< 0.125f)
        {
            controllerRoation.y = 0;
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
        rb.AddRelativeForce(new Vector3(move.x, 0, move.y) * movementSpeed, ForceMode.Force);
    }
    private void Update()
    {
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

}
