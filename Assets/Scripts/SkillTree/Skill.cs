using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int skillCost;


    public SkillTreeManager skillTreeManager;

    public string gameObjectWithScript;
    public string scriptName;
    public string methodName;
    public float amount;
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
        if (skillTreeManager.skillPoints >= skillCost)
        {
            skillTreeManager.skillPoints -= skillCost;
            GetComponent<Button>().interactable = false;
            GameObject.Find(gameObjectWithScript).GetComponent(scriptName).BroadcastMessage(methodName);
        }
    }

}
