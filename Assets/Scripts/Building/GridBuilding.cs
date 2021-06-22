using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GridBuilding : MonoBehaviour
{
    //how far
    public float buildDistance;
    //is wall or ground
    public LayerMask buildLayers;
    //marker thing
    public GameObject marker;
    public Transform buildPoint;
    //controls
    public InputMaster controlls;
    //ui
    public Toggle buildToggle;
    //list of towers and buidling on or off
    bool build;
    public List<GameObject> towers;
    //what building
    int index;
    //building types
    public Image buildingIconColour;
    public Image buildingIconImage;
    int lastIndex;
    //how much?
    public Text BuildingCostText;

    private void Awake()
    {
        //SET THE CONTROLS
        controlls = new InputMaster();
        controlls.Player.EnableBuilding.performed += ctx => build = !build;
        controlls.Player.EnableDestoryBuilding.performed += ctx => build = false;
        controlls.Player.Scrollwheel.performed += ctx => index += (int)ctx.ReadValue<float>() / 120;
    }
    private void OnEnable()
    {
        //ENABLE THE CONTROLS
        controlls.Enable();
    }


    void Update()
    {
        //make sure the index is within 0 to the amount of towers - 1
        if (index > towers.Count - 1)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = towers.Count - 1;
        }
        if (build)
            {
            //set the ui to the colour and icon and the cost amount 
            lastIndex = index;
            buildingIconColour.color = towers[index].GetComponent<Building>().buildingColour;
            buildingIconColour.transform.parent.gameObject.SetActive(true);

            buildingIconImage.sprite = towers[index].GetComponent<Building>().buildingIcon;
            buildingIconImage.transform.parent.gameObject.SetActive(true);

            BuildingCostText.gameObject.SetActive(true);
            BuildingCostText.text = "-" + towers[index].GetComponent<Building>().costAmount.ToString();
            //check if the object is on the floor
            marker.GetComponent<GroundCheck>().canBePlacedOnTheFloor = towers[index].GetComponent<Building>().canBePlacedOnFloor;
            marker.GetComponent<GroundCheck>().canBePlacedOnTheWall = towers[index].GetComponent<Building>().canBePlacedOnWall;
            marker.GetComponent<GroundCheck>().buildingCost = towers[index].GetComponent<Building>().costAmount;
            Gamepad gp = InputSystem.GetDevice<Gamepad>();
            if (gp != null)
            {
                //change building
                if (gp.leftShoulder.wasPressedThisFrame)
                {
                    index -= 1;
                }

                if (gp.rightShoulder.wasPressedThisFrame)
                {
                    index += 1;
                }
            }

    
            //ui and marker
            buildToggle.isOn = true;
            marker.SetActive(true);
            //be the same as the camera
            buildPoint.transform.rotation = Camera.main.transform.rotation;
            RaycastHit hit;
            if (Physics.Raycast(buildPoint.transform.position, buildPoint.transform.forward, out hit, buildDistance, buildLayers))
            {
                //do math
                marker.transform.position = SnapPosition(hit.point, 4);
                //look the correct way
                Vector3 aimingDir = new Vector3(hit.point.x, hit.point.y, hit.point.z) - marker.transform.position;

                float angle = -Mathf.Atan2(aimingDir.z, aimingDir.x) * Mathf.Rad2Deg + 90.0f;
                angle = Mathf.Round(angle / 90.0f) * 90.0f;

                Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.up);
                //the maker will now work on a wall at any angle
                marker.transform.rotation = qTo;
            }

            if (marker.GetComponent<GroundCheck>().objectPlaced == false && marker.GetComponent<GroundCheck>().canBuild)
            {
                Mouse m = InputSystem.GetDevice<Mouse>();

                if (m != null)
                {
                    if (m.leftButton.wasPressedThisFrame)
                    {
                        //place the building on a wall or the floor
                        Debug.Log("leftClick");
                        if(marker.GetComponent<GroundCheck>().wall == true && towers[index].GetComponent<Building>().canBePlacedOnWall)
                        {
                            GameObject go = Instantiate(towers[index], marker.transform.position, marker.transform.rotation);
                            go.GetComponent<Building>().Create(marker.GetComponent<GroundCheck>().wall);
                            FindObjectOfType<GameManager>().goldAmount -= towers[index].GetComponent<Building>().costAmount;
                        }

                        if (marker.GetComponent<GroundCheck>().floor == true && towers[index].GetComponent<Building>().canBePlacedOnFloor)
                        {
                            GameObject go = Instantiate(towers[index], marker.transform.position, marker.transform.rotation);
                            go.GetComponent<Building>().Create(marker.GetComponent<GroundCheck>().wall);
                            FindObjectOfType<GameManager>().goldAmount -= towers[index].GetComponent<Building>().costAmount;
                        }

                    }
                }
                if (gp != null)
                {
                    
                    if (gp.buttonWest.wasPressedThisFrame)
                    {
                        //same as above just for console
                        Debug.Log("westbutton");
                        if (marker.GetComponent<GroundCheck>().wall == true && towers[index].GetComponent<Building>().canBePlacedOnWall)
                        {
                            GameObject go = Instantiate(towers[index], marker.transform.position, marker.transform.rotation);
                            go.GetComponent<Building>().Create(marker.GetComponent<GroundCheck>().wall);
                            FindObjectOfType<GameManager>().goldAmount -= towers[index].GetComponent<Building>().costAmount;
                        }

                        if (marker.GetComponent<GroundCheck>().floor == true && towers[index].GetComponent<Building>().canBePlacedOnFloor)
                        {
                            GameObject go = Instantiate(towers[index], marker.transform.position, marker.transform.rotation);
                            go.GetComponent<Building>().Create(marker.GetComponent<GroundCheck>().wall);
                            FindObjectOfType<GameManager>().goldAmount -= towers[index].GetComponent<Building>().costAmount;
                        }

                    }
                }
            }
        }
        else
        {
            //if no build be sad
            index = lastIndex;
            buildingIconColour.transform.parent.gameObject.SetActive(false);
            buildingIconImage.transform.parent.gameObject.SetActive(false);
            BuildingCostText.gameObject.SetActive(false);

            buildToggle.isOn = false;
            marker.SetActive(false);
        }
    }

    //do the snap
    Vector3 SnapPosition(Vector3 input, float factor = 4)
    {
        //get the input lets say 4,0,4 || ok add 1 to y (4,1,4) || divide by 4 (1,.25,1) || time by 4 = (4,1,4) the same thing as the start, after writing this i reasile this wasn't a good example
        input.y = input.y+ 1;
        float x = Mathf.Round(input.x / factor) * factor;
        float y = Mathf.Round(input.y / factor) * factor;
        float z = Mathf.Round(input.z / factor) * factor;
        //returne the vaule 
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        //lazers
        Gizmos.DrawRay(buildPoint.transform.position, buildPoint.transform.forward);
    }

}
