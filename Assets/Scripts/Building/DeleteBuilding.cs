using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeleteBuilding : MonoBehaviour
{
    public GameObject deleteMarker;
    public InputMaster controlls;
    public bool deleteMode;

    float destroyDistance;
    Transform buildPoint;
    public Toggle deleteToggle;

    private void Awake()
    {
        controlls = new InputMaster();
        controlls.Player.EnableDestoryBuilding.performed += ctx => deleteMode = !deleteMode;
        controlls.Player.EnableBuilding.performed += ctx => deleteMode = false;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }

    private void Start()
    {
        destroyDistance = gameObject.GetComponent<GridBuilding>().buildDistance;
        buildPoint = gameObject.GetComponent<GridBuilding>().buildPoint;
    }

    void Update()
    {
        deleteToggle.isOn = deleteMode;
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        Mouse m = InputSystem.GetDevice<Mouse>();

        if (deleteMode)
        {

            RaycastHit hit;
            if (Physics.Raycast(buildPoint.transform.position, buildPoint.transform.forward, out hit, destroyDistance))
            {
                if (hit.transform.tag == "Building")
                {
                    
                     if (hit.transform.GetComponentInParent<Building>())
                    {
                        if (hit.transform.GetComponentInParent<Building>().placedOnWall)
                        {
                            deleteMarker.transform.position = new Vector3(hit.transform.GetComponentInParent<Building>().check.transform.position.x, hit.transform.GetComponentInParent<Building>().check.transform.position.y, hit.transform.GetComponentInParent<Building>().check.transform.position.z);
                        }
                        else
                        {
                            deleteMarker.transform.position = new Vector3(hit.transform.GetComponentInParent<Building>().check.transform.position.x, hit.transform.GetComponentInParent<Building>().check.transform.position.y + 4, hit.transform.GetComponentInParent<Building>().check.transform.position.z);
                        }
                        if (m != null)
                        {
                            if (m.leftButton.wasPressedThisFrame)
                            {
                                FindObjectOfType<GameManager>().goldAmount += hit.transform.GetComponentInParent<Building>().costAmount / 2;
                                Destroy(hit.transform.GetComponentInParent<Building>().transform.gameObject);
                            }
                        }
                        if (gp != null)
                        {
                            if (gp.buttonWest.wasPressedThisFrame)
                            {
                                FindObjectOfType<GameManager>().goldAmount += hit.transform.GetComponentInParent<Building>().costAmount / 2;
                                Destroy(hit.transform.GetComponentInParent<Building>().transform.gameObject);
                            }
                        }
                    }
                    deleteMarker.SetActive(true);
                }
                else
                {
                    deleteMarker.SetActive(false);

                }
            }
            else
            {
                deleteMarker.SetActive(false);

            }

        }
        else
        {
            deleteMarker.SetActive(false);
        }
    }
}
