using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //this is a interseting hack of a job
    public Transform wallCheck;

    public bool canBuild;
    //stats 
    public Color canPlaceColour;
    public Color canNotPlaceColour;
    public bool wall;
    public bool floor;

    //more stuff
    public GameObject floorLook;
    public GameObject wallLook;
    public bool objectPlaced;
    public BoxCollider check;

    public bool canBePlacedOnTheWall;
    public bool canBePlacedOnTheFloor;

    public int buildingCost;
    int goldAmount;

    void Update()
    {
        //find how much the building is
        goldAmount = FindObjectOfType<GameManager>().goldAmount;


        RaycastHit hit;
        if (Physics.Raycast(wallCheck.transform.position, wallCheck.up, out hit, 4))////////////////////////////////////
        {
            //make sure building is enough
            if (goldAmount >= buildingCost)
            {
                //make the look go to the floor and be blue
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
                else//make the looks go on floor and be red and no place
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
        else//no can doo doo
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canNotPlaceColour;

                }
                floorLook.SetActive(true);
                wallLook.SetActive(false);
                objectPlaced = false;
                canBuild = false;
                floor = true;
                wall = false;
            }
        }
        else if (Physics.Raycast(wallCheck.transform.position, wallCheck.forward, out hit, 4))////////////////////////////////////////////
        {
            if (goldAmount >= buildingCost)
            {
                if (canBePlacedOnTheWall)// same as floor but wall insted
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
                else//can't place it there my lord
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
            else//your not allowed 
            {
                foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material.color = canNotPlaceColour;
                }
                wall = true;
                floor = false;
                objectPlaced = false;
                canBuild = false;

                wallLook.SetActive(true);
                floorLook.SetActive(false);
            }
        }//nothing
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
        //check if something is there if so no can not do
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
