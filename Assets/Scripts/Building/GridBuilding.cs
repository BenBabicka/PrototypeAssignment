using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GridBuilding : MonoBehaviour
{
    public float buildDistance;
    public LayerMask buildLayers;
    public GameObject marker;
    public Transform buildPoint;

    public InputMaster controlls;

    public Toggle buildToggle;

    bool build;
    public List<GameObject> towers;

    int index;

    public Image buildingIconColour;
    public Image buildingIconImage;
    int lastIndex;

    public Text BuildingCostText;

    private void Awake()
    {
        controlls = new InputMaster();
        controlls.Player.EnableBuilding.performed += ctx => build = !build;
        controlls.Player.EnableDestoryBuilding.performed += ctx => build = false;
        controlls.Player.Scrollwheel.performed += ctx => index += (int)ctx.ReadValue<float>() / 120;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }


    void Update()
    {
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

            lastIndex = index;
            buildingIconColour.color = towers[index].GetComponent<Building>().buildingColour;
            buildingIconColour.transform.parent.gameObject.SetActive(true);

            buildingIconImage.sprite = towers[index].GetComponent<Building>().buildingIcon;
            buildingIconImage.transform.parent.gameObject.SetActive(true);

            BuildingCostText.gameObject.SetActive(true);
            BuildingCostText.text = "-" + towers[index].GetComponent<Building>().costAmount.ToString();

            marker.GetComponent<GroundCheck>().canBePlacedOnTheFloor = towers[index].GetComponent<Building>().canBePlacedOnFloor;
            marker.GetComponent<GroundCheck>().canBePlacedOnTheWall = towers[index].GetComponent<Building>().canBePlacedOnWall;
            marker.GetComponent<GroundCheck>().buildingCost = towers[index].GetComponent<Building>().costAmount;
            Gamepad gp = InputSystem.GetDevice<Gamepad>();
            if (gp != null)
            {
                if (gp.leftShoulder.wasPressedThisFrame)
                {
                    index -= 1;
                }

                if (gp.rightShoulder.wasPressedThisFrame)
                {
                    index += 1;
                }
            }

    

            buildToggle.isOn = true;
            marker.SetActive(true);
            buildPoint.transform.rotation = Camera.main.transform.rotation;
            RaycastHit hit;
            if (Physics.Raycast(buildPoint.transform.position, buildPoint.transform.forward, out hit, buildDistance, buildLayers))
            {
                marker.transform.position = SnapPosition(hit.point, 4);

                Vector3 aimingDir = new Vector3(hit.point.x, hit.point.y, hit.point.z) - marker.transform.position;

                float angle = -Mathf.Atan2(aimingDir.z, aimingDir.x) * Mathf.Rad2Deg + 90.0f;
                angle = Mathf.Round(angle / 90.0f) * 90.0f;

                Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.up);

                marker.transform.rotation = qTo;
            }

            if (marker.GetComponent<GroundCheck>().objectPlaced == false && marker.GetComponent<GroundCheck>().canBuild)
            {
                Mouse m = InputSystem.GetDevice<Mouse>();

                if (m != null)
                {
                    if (m.leftButton.wasPressedThisFrame)
                    {
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
            index = lastIndex;
            buildingIconColour.transform.parent.gameObject.SetActive(false);
            buildingIconImage.transform.parent.gameObject.SetActive(false);
            BuildingCostText.gameObject.SetActive(false);

            buildToggle.isOn = false;
            marker.SetActive(false);
        }
    }

    Vector3 SnapPosition(Vector3 input, float factor = 4)
    {
        input.y = input.y+ 1;
        float x = Mathf.Round(input.x / factor) * factor;
        float y = Mathf.Round(input.y / factor) * factor;
        float z = Mathf.Round(input.z / factor) * factor;

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(buildPoint.transform.position, buildPoint.transform.forward);
    }

}
