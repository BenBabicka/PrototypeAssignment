using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon")]
public class weapon : ScriptableObject
{
    public new string name;
    public string discription;

    public float lightDamage;
    public float heavyDamage;

    public float lightStamina;
    public float heavyStamina;

    public float lightAttackTime;
    public float heavyAttackTime;

    public enum WeaponType { melee, archey, magic}
    public WeaponType weaponType;

    public GameObject projectile;

    public float range;
}
