using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    //not really a manager but ok
    //how man points
    public int skillPoints;
    //the ui
    public GameObject skillPointsUI;

    void Update()
    {
        //set the ui
        if(skillPoints > 0)
        {
            skillPointsUI.SetActive(true);
            skillPointsUI.GetComponentInChildren<Text>().text = skillPoints.ToString();
        }
        else
        {
            skillPointsUI.SetActive(false);
        }
    }
}
