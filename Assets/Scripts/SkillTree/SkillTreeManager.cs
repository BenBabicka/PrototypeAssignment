using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{

    public int skillPoints;

    public GameObject skillPointsUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
