using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public weapon meleeWeapon;
    public weapon archerWeapon;
    public weapon mageWeapon;

    public InputMaster controlls;

    bool leftClick;
    bool rightClick;

    public enum AttackType {melee,archer,mage}
    public AttackType attackType;

    private int weaponSelectMode = -1;

    public Transform firepoint;
    public Transform meleeAttackPoint;
    float timer;


    public bool buildMode;

    public Toggle meleeToggle;
    public Toggle archeryToggle;
    public Toggle mageToggle;
    private void Awake()
    {
        controlls = new InputMaster();

        controlls.Player.QuickAttack.performed += ctx => leftClick = true;
        controlls.Player.QuickAttack.canceled += ctx => leftClick = false;


        controlls.Player.HeavyAttack.performed += ctx => rightClick = true;
        controlls.Player.HeavyAttack.canceled += ctx => rightClick = true;

        controlls.Player.Scrollwheel.performed += ctx => weaponSelectMode += (int) ctx.ReadValue<float>() / 120;

        controlls.Player.EnableBuilding.performed += ctx => buildMode = !buildMode;

    }

    private void OnEnable()
    {
        controlls.Enable();
    }



    void Update()
    {

        firepoint.transform.rotation = GetComponentInChildren<Camera>().gameObject.transform.rotation;
        meleeAttackPoint.transform.rotation = GetComponentInChildren<Camera>().gameObject.transform.rotation;

        if (!buildMode)
        {
            Gamepad gp = InputSystem.GetDevice<Gamepad>();
            if (gp != null)
            {
                if (gp.leftShoulder.wasPressedThisFrame)
                {
                    weaponSelectMode -= 1;
                }

                if (gp.rightShoulder.wasPressedThisFrame)
                {
                    weaponSelectMode += 1;
                }
            }          

            if (weaponSelectMode > 1)
            {
                weaponSelectMode = -1;
            }
            if (weaponSelectMode < -1)
            {
                weaponSelectMode = 1;
            }


            if (weaponSelectMode == -1)
            {
                attackType = AttackType.melee;
                meleeToggle.isOn = true;
                archeryToggle.isOn = false;
                mageToggle.isOn = false;
            }
            if (weaponSelectMode == 0)
            {
                attackType = AttackType.archer;
                meleeToggle.isOn = false;
                archeryToggle.isOn = true;
                mageToggle.isOn = false;
            }
            if (weaponSelectMode == 1)
            {
                attackType = AttackType.mage;
                meleeToggle.isOn = false;
                archeryToggle.isOn = false;
                mageToggle.isOn = true;
            }
            if (leftClick)
            {
                LeftClick();
            }
            if (rightClick)
            {
                RightClick();
            }
            timer += Time.deltaTime;
        }
    }


    public void LeftClick()
    {
        if(attackType == AttackType.melee)
        {
            RaycastHit hit;
            if(Physics.Raycast(meleeAttackPoint.position, meleeAttackPoint.forward, out hit, meleeWeapon.range))
            {
                if (timer > meleeWeapon.lightAttackTime)
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(meleeWeapon.lightDamage);
                    }
                    timer = 0;
                }
            }
        }

        if (attackType == AttackType.archer)
        {
            if (timer > archerWeapon.lightAttackTime)
            {
               GameObject Go = Instantiate(archerWeapon.projectile, firepoint.position, firepoint.rotation) as GameObject;
                Go.GetComponent<Projectile>().timeTillDeath = archerWeapon.range;
                Go.GetComponent<Projectile>().damage = archerWeapon.lightDamage;
                timer = 0;

            }
        }

        if (attackType == AttackType.mage)
        {
            if (timer > mageWeapon.lightAttackTime)
            {
                GameObject Go = Instantiate(mageWeapon.projectile, firepoint.position, firepoint.rotation)as GameObject;
                Go.GetComponent<Projectile>().timeTillDeath = mageWeapon.range;
                Go.GetComponent<Projectile>().damage = mageWeapon.lightDamage;
                timer = 0;

            }
        }
    }

    public void RightClick()
    {
        if (attackType == AttackType.melee)
        {
            if (timer > meleeWeapon.heavyAttackTime)
            {
                RaycastHit hit;
                if (Physics.Raycast(meleeAttackPoint.position, meleeAttackPoint.forward, out hit, meleeWeapon.range))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(meleeWeapon.heavyDamage);
                    }
                }
                timer = 0;
            }
        }
    }
}
