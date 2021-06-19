using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject floorGameobject;
    public GameObject wallGameobject;

    public int costAmount;


    public Color buildingColour;
    public Sprite buildingIcon;
    public float attackRate;
    public float damage;
    public float range;

    public bool canBePlacedOnFloor;
    public bool canBePlacedOnWall;

    public GameObject check;

    public bool placedOnWall;

    public void Create(bool wall)
    {
        placedOnWall = wall;
        if (wall)
        {
            wallGameobject.SetActive(true);
        }
        else
        {
            floorGameobject.SetActive(true);
        }
    }
}
