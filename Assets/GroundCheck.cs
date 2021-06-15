using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Transform wallCheck;

    public bool canBuild;

    public Color canPlaceColour;
    public Color canNotPlaceColour;
    public bool wall;
    public bool floor;
    public GameObject floorLook;
    public GameObject wallLook;
    public bool objectPlaced;
    public BoxCollider check;

    public bool canBePlacedOnTheWall;
    public bool canBePlacedOnTheFloor;

    void Update()
    {
        


        RaycastHit hit;
        if (Physics.Raycast(wallCheck.transform.position, wallCheck.up, out hit, 4))
        {
            if (canBePlacedOnTheFloor)
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canPlaceColour;
                }
                floorLook.SetActive(true);
                wallLook.SetActive(false);
                objectPlaced = false;

                floor = true;
                wall = false;
                canBuild = true;
            }
            else
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canNotPlaceColour;
             
                }
                floorLook.SetActive(true);
                wallLook.SetActive(false);
                objectPlaced = false;

                floor = true;
                wall = false;
            }
        }
        else if (Physics.Raycast(wallCheck.transform.position, wallCheck.forward, out hit, 4))
        {
            if (canBePlacedOnTheWall)
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canPlaceColour;
                }
                wall = true;
                floor = false;
                objectPlaced = false;

                wallLook.SetActive(true);
                floorLook.SetActive(false);

                canBuild = true;
            }
            else
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canNotPlaceColour;
                }
                wall = true;
                floor = false;
                objectPlaced = false;

                wallLook.SetActive(true);
                floorLook.SetActive(false);
            }
        }
        else
        {
            foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                item.material.color = canNotPlaceColour;
            }
            wallLook.SetActive(false);
            floorLook.SetActive(false);

            canBuild = false;
            wall = false;
            floor = false;
        }
        Collider[] boxs = Physics.OverlapBox(check.transform.position, check.bounds.size / 2);
        foreach (var col in boxs)
        {
            if(col.tag == "Building")
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canNotPlaceColour;
                }
                objectPlaced = true;
            }
          
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(wallCheck.transform.position, wallCheck.forward);
    }
}
