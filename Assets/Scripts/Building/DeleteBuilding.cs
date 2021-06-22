using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeleteBuilding : MonoBehaviour
{
    //delete cube red thing
    public GameObject deleteMarker;
    //PlayerControls
    public InputMaster controlls;
    //to delete or not to delete
    public bool deleteMode;
    //how far is ones delete ablity
    float destroyDistance;
    //from a point of view
    Transform buildPoint;
    //looks
    public Toggle deleteToggle;

    private void Awake()
    {//set the controls up
        controlls = new InputMaster();
        controlls.Player.EnableDestoryBuilding.performed += ctx => deleteMode = !deleteMode;
        controlls.Player.EnableBuilding.performed += ctx => deleteMode = false;
    }
    private void OnEnable()
    {//enable the controls
        controlls.Enable();
    }

    private void Start()
    {//set the stats jim
        destroyDistance = gameObject.GetComponent<GridBuilding>().buildDistance;
        buildPoint = gameObject.GetComponent<GridBuilding>().buildPoint;
    }

    void Update()
    {
        //make toggle on or off if deletey
        deleteToggle.isOn = deleteMode;
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        Mouse m = InputSystem.GetDevice<Mouse>();
        //now it begins
        if (deleteMode)
        {
            //shoot lazer
            RaycastHit hit;
            if (Physics.Raycast(buildPoint.transform.position, buildPoint.transform.forward, out hit, destroyDistance))
            {//hit a building?
                if (hit.transform.tag == "Building")
                {
                    //now find that script
                     if (hit.transform.GetComponentInParent<Building>())
                    {
                        //set the position of red box to the position of the building
                        if (hit.transform.GetComponentInParent<Building>().placedOnWall)
                        {
                            deleteMarker.transform.position = new Vector3(hit.transform.GetComponentInParent<Building>().check.transform.position.x, hit.transform.GetComponentInParent<Building>().check.transform.position.y, hit.transform.GetComponentInParent<Building>().check.transform.position.z);
                        }
                        else
                        {
                            deleteMarker.transform.position = new Vector3(hit.transform.GetComponentInParent<Building>().check.transform.position.x, hit.transform.GetComponentInParent<Building>().check.transform.position.y + 4, hit.transform.GetComponentInParent<Building>().check.transform.position.z);
                        }
                        //delete with mouse (get half your money back)
                        if (m != null)
                        {
                            if (m.leftButton.wasPressedThisFrame)
                            {
                                FindObjectOfType<GameManager>().goldAmount += hit.transform.GetComponentInParent<Building>().costAmount / 2;
                                Destroy(hit.transform.GetComponentInParent<Building>().transform.gameObject);
                            }
                        }
                        //just like the mouse but with a controller (same as last time on 50% back sorry, exchange rate no good)
                        if (gp != null)
                        {
                            if (gp.buttonWest.wasPressedThisFrame)
                            {
                                FindObjectOfType<GameManager>().goldAmount += hit.transform.GetComponentInParent<Building>().costAmount / 2;
                                Destroy(hit.transform.GetComponentInParent<Building>().transform.gameObject);
                            }
                        }
                    }
                     //make red true
                    deleteMarker.SetActive(true);
                }
                else
                {
                    //make red hide
                    deleteMarker.SetActive(false);

                }
            }
            else
            {       //make red hide

                deleteMarker.SetActive(false);

            }

        }
        else
        {    //make red hide

            deleteMarker.SetActive(false);
        }
    }
}
