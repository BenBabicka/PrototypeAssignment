using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //level xp and next level text
    public int playerLevel;
    public int xp = 0;
    public int amountOfXpNeed = 1000;
    public float increaseAmount;
    //UI
    [Header("UI")]
    public Slider xpBar;
    public Text xpText;

   

    void Update()
    {   //set ui
        xpBar.maxValue = amountOfXpNeed;
        xpBar.value = xp;
        xpText.text = "XP : " + xp + " / " + amountOfXpNeed;
        //level up
        if(xp >= amountOfXpNeed)
        {
            LevelUp();
        }
    }

    //yay
    void LevelUp()
    {
        //i feel good.
        xp = 0;
        float nextXpLevel = (float)amountOfXpNeed * increaseAmount;
        amountOfXpNeed = (int)nextXpLevel;
        FindObjectOfType<SkillTreeManager>().skillPoints++;
        playerLevel++;
    }

}
