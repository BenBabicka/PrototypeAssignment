using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int skillCost;

    [HideInInspector]
    public SkillTreeManager skillTreeManager;

    public string gameObjectWithScript;
    public string scriptName;
    public string methodName;

    public bool isBrought;
    public List<Skill> skillsNeeded;

    bool canBuy = true;

    void Start()
    {
        skillTreeManager = FindObjectOfType<SkillTreeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuySkillPoint()
    {
        for (int i = 0; i < skillsNeeded.Count;)
        {
            if(skillsNeeded[i].isBrought)
            {
                if(i >= skillsNeeded.Count -1)
                {
                    canBuy = true;
                }
                Debug.Log("Skill has been purchased");
                i++;
            }
            else
            {
                Debug.Log("Skill required are not purchased");
                canBuy = false;
                break;
            }
        }
        if (canBuy)
        {
            if (skillTreeManager.skillPoints >= skillCost)
            {
                skillTreeManager.skillPoints -= skillCost;
                GetComponent<Button>().interactable = false;
                isBrought = true;
                GameObject.Find(gameObjectWithScript).GetComponent(scriptName).BroadcastMessage(methodName);
            }
        }
    }

}
