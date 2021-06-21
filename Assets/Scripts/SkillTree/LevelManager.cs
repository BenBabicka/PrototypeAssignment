using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int playerLevel;
    public int xp = 0;
    public int amountOfXpNeed = 1000;
    public float increaseAmount;
    [Header("UI")]
    public Slider xpBar;
    public Text xpText;

   

    void Update()
    {
        xpBar.maxValue = amountOfXpNeed;
        xpBar.value = xp;
        xpText.text = "XP : " + xp + " / " + amountOfXpNeed;

        if(xp >= amountOfXpNeed)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        xp = 0;
        float nextXpLevel = (float)amountOfXpNeed * increaseAmount;
        amountOfXpNeed = (int)nextXpLevel;
        FindObjectOfType<SkillTreeManager>().skillPoints++;
        playerLevel++;
    }

}
