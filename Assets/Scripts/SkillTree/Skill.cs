using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    //skils cost
    public int skillCost;
    //shh im being sneaky
    [HideInInspector]
    public SkillTreeManager skillTreeManager;
    //really cool variables that will be explained
    public string gameObjectWithScript;
    public string scriptName;
    public string methodName;

    //checkers
    public bool isBrought;
    public List<Skill> skillsNeeded;
    bool canBuy = true;

    //sad colour
    public Color isBoughtColour;

    void Start()
    {//finding nemo
        skillTreeManager = FindObjectOfType<SkillTreeManager>();
    }
    private void Update()
    {
        //checking to make it sad
        if(isBrought)
        {
            gameObject.GetComponent<Image>().color = isBoughtColour;
        }
    }

    //EPIC BUYING
    public void BuySkillPoint()
    {
        //quick check if in store (waiting.... waiting.... waiting...)
        if (!isBrought)
        {
            //epic list check to see if the skills required have been brought
            for (int i = 0; i < skillsNeeded.Count;)
            {
                if (skillsNeeded[i].isBrought)
                {
                    if (i >= skillsNeeded.Count - 1)
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
            //if list is ok do the buying
            if (canBuy)
            {
                //well i lied by after there are enough skill points, sorry :(
                if (skillTreeManager.skillPoints >= skillCost)
                {
                    //buy money hello cool thing
                    skillTreeManager.skillPoints -= skillCost;
                    isBrought = true;
                    //VERY COOL PART. find the object, get the component, yeah followin me, and then call the method. I AM BIG BRAIN
                    GameObject.Find(gameObjectWithScript).GetComponent(scriptName).BroadcastMessage(methodName);
                }
            }
        }
    }

}
