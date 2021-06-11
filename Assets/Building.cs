using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject floorGameobject;
    public GameObject wallGameobject;

    public Color buildingColour;
    public Sprite buildingIcon;
    public float attackRate;
    public float damage;

    public bool canBePlacedOnFloor;
    public bool canBePlacedOnWall;

    public void Create(bool wall)
    {
        if(wall)
        {
            wallGameobject.SetActive(true);
        }
        else
        {
            floorGameobject.SetActive(true);
        }
    }
}
