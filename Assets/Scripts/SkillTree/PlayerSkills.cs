using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
 public void SkillOffence()
    {
        gameObject.GetComponent<PlayerController>().walkSpeed += 20;
    }
    public void SkillDefence ()
    {
        gameObject.GetComponent<PlayerHealth>().maxHealth += 50;
        gameObject.GetComponent<PlayerHealth>().health += 50;
    }
    public void SkillUpgradeTower()
    {
        foreach (var item in FindObjectsOfType<Building>())
        {
            item.attackRate -= .1f;
            item.damage += .5f;
            item.range += 5f;
        }
    }

    public void SkillMeleeUpgrade()
    {
        gameObject.GetComponent<PlayerCombat>().meleeWeapon.lightDamage += 2;
        gameObject.GetComponent<PlayerCombat>().meleeWeapon.heavyDamage += 5;
        gameObject.GetComponent<PlayerCombat>().meleeWeapon.heavyAttackTime -= .3f;
    }

    public void SkillArcherUpgrade()
    {
        gameObject.GetComponent<PlayerCombat>().archerWeapon.lightDamage += 2;
        gameObject.GetComponent<PlayerCombat>().archerWeapon.range += 10;
        gameObject.GetComponent<PlayerCombat>().archerWeapon.lightAttackTime -= .05f;
    }
    public void SkillMageUpgrade()
    {
        gameObject.GetComponent<PlayerCombat>().mageWeapon.lightDamage += 10;
        gameObject.GetComponent<PlayerCombat>().mageWeapon.range += 20;
        gameObject.GetComponent<PlayerCombat>().mageWeapon.lightAttackTime -= .5f;
    }
}
