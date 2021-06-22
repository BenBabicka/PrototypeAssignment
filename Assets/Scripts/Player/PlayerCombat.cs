using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    //Weapons stat holders
    public weapon meleeWeapon;
    public weapon archerWeapon;
    public weapon mageWeapon;

    //PlayerControls
    public InputMaster controlls;
    //Left or right clicking
    bool leftClick;
    bool rightClick;
    //Attack mode
    public enum AttackType {melee,archer,mage}
    public AttackType attackType;

    //What weapon is selected
    private int weaponSelectMode = -1;
    //Attack locations
    public Transform firepoint;
    public Transform meleeAttackPoint;

    float timer;

    //If building or destory
    public bool buildMode;
    public bool destoryMode;

    //UI stuff to look "good"
    public Toggle meleeToggle;
    public Toggle archeryToggle;
    public Toggle mageToggle;

    //sparkels
    public GameObject meleeParticle;

    //mega sliders to show the persons
    public Slider quickAttackSlider;
    public Slider heavyAttackSlider;

    private void Awake()
    {
        //controll setup
        controlls = new InputMaster();

        controlls.Player.QuickAttack.performed += ctx => leftClick = true;
        controlls.Player.QuickAttack.canceled += ctx => leftClick = false;


        controlls.Player.HeavyAttack.performed += ctx => rightClick = true;
        controlls.Player.HeavyAttack.canceled += ctx => rightClick = false;

        controlls.Player.Scrollwheel.performed += ctx => weaponSelectMode += (int) ctx.ReadValue<float>() / 120;

        controlls.Player.EnableBuilding.performed += ctx => buildMode = !buildMode;
        controlls.Player.EnableBuilding.performed += ctx => destoryMode = false;

        controlls.Player.EnableDestoryBuilding.performed += ctx => destoryMode = !destoryMode;
        controlls.Player.EnableDestoryBuilding.performed += ctx => buildMode = false;

    }

    private void OnEnable()
    {
        //enable
        controlls.Enable();
    }



    void Update()
    {
        //makes the rotation of the firepoint be the same rotation as the camera (UP AND DOWN)
        firepoint.transform.rotation = GetComponentInChildren<Camera>().gameObject.transform.rotation;
        meleeAttackPoint.transform.rotation = GetComponentInChildren<Camera>().gameObject.transform.rotation;
       //Slidy things to make thing look cool
        if (timer < quickAttackSlider.maxValue)
        {
            quickAttackSlider.value = timer;
        }
        else
        {
            quickAttackSlider.value = quickAttackSlider.maxValue;
        }
        //Make slidy things not spaz out when the timer is at max value
        if (timer < heavyAttackSlider.maxValue)
        {
            heavyAttackSlider.value = timer;
        }
        else
        {
            heavyAttackSlider.value = heavyAttackSlider.maxValue;
        }
        //COMBATE MODE ENAGED (after a check to make sure there not building or destory of course)
        if (!buildMode && !destoryMode)
        {
            //Weapon selection mode
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
            // still weapon selection mode (go less the  -1 go 1, go more then 1 go -1, easy?)
            if (weaponSelectMode > 1)
            {
                weaponSelectMode = -1;
            }
            if (weaponSelectMode < -1)
            {
                weaponSelectMode = 1;
            }

            //still weapon selection mode (this time to change visuals and timers)
            if (weaponSelectMode == -1)
            {
                attackType = AttackType.melee;
                quickAttackSlider.maxValue = meleeWeapon.lightAttackTime;
                heavyAttackSlider.maxValue = meleeWeapon.heavyAttackTime;
                meleeToggle.isOn = true;
                archeryToggle.isOn = false;
                mageToggle.isOn = false;
            }
            if (weaponSelectMode == 0)
            {
                attackType = AttackType.archer;
                quickAttackSlider.maxValue = archerWeapon.lightAttackTime;
                heavyAttackSlider.maxValue = archerWeapon.heavyAttackTime;
                meleeToggle.isOn = false;
                archeryToggle.isOn = true;
                mageToggle.isOn = false;
            }
            if (weaponSelectMode == 1)
            {
                attackType = AttackType.mage;
                quickAttackSlider.maxValue = mageWeapon.lightAttackTime;
                heavyAttackSlider.maxValue = mageWeapon.heavyAttackTime;
                meleeToggle.isOn = false;
                archeryToggle.isOn = false;
                mageToggle.isOn = true;
            }
            //it says it in the name (LEFT OR RIGHT CLICK)
            if (leftClick)
            {
                LeftClick();
            }
            if (rightClick)
            {
                RightClick();
            }
        }
        //Timer for attack go brrrr
        timer += Time.deltaTime;

    }

    //LEFT CLICK ATTACK MODE (Yay!)
    public void LeftClick()
    {
        //checking the mode
        if(attackType == AttackType.melee)
        {
            //use lazers to see if it hits something
            RaycastHit hit;
            if(Physics.Raycast(meleeAttackPoint.position, meleeAttackPoint.forward, out hit, meleeWeapon.range))
            {
                if (timer > meleeWeapon.lightAttackTime)
                {
                    
                    if (hit.transform.tag == "Enemy")
                    {
                        //do damage and sparkels if it hit enemy 
                        GameObject go = Instantiate(meleeParticle, hit.point, Quaternion.identity) as GameObject;
                        go.transform.LookAt(transform);
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(meleeWeapon.lightDamage);
                    }
                    //make time 0 to make it go brrrr
                    timer = 0;

                }
            }
        }
        //check the attack mode
        if (attackType == AttackType.archer)
        {
            if (timer > archerWeapon.lightAttackTime)
            {//spawn ball of death and hurt baddies
               GameObject Go = Instantiate(archerWeapon.projectile, firepoint.position, firepoint.rotation) as GameObject;
                Go.GetComponent<Projectile>().timeTillDeath = archerWeapon.range;
                Go.GetComponent<Projectile>().damage = archerWeapon.lightDamage;
                timer = 0;

            }
        }
        //check attack mode
        if (attackType == AttackType.mage)
        {
            if (timer > mageWeapon.lightAttackTime)
            {//same as last one spawn ball of death
                GameObject Go = Instantiate(mageWeapon.projectile, firepoint.position, firepoint.rotation)as GameObject;
                Go.GetComponent<Projectile>().timeTillDeath = mageWeapon.range;
                Go.GetComponent<Projectile>().damage = mageWeapon.lightDamage;
                timer = 0;

            }
        }
    }
    //RIGHT CLICK ATTACK MODE
    public void RightClick()
    {//check if the person is meleeing, (is meleeing a word)
        if (attackType == AttackType.melee)
        {
            if (timer > meleeWeapon.heavyAttackTime)
            {
                //lazer beams again
                RaycastHit hit;

                if (Physics.Raycast(meleeAttackPoint.position, meleeAttackPoint.forward, out hit, meleeWeapon.range))
                {// hit the enemy, do damage, WOW
                   
                    if (hit.transform.tag == "Enemy")
                    {
                        GameObject go = Instantiate(meleeParticle, hit.point, Quaternion.identity) as GameObject;
                        go.transform.LookAt(transform);
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(meleeWeapon.heavyDamage);
                    }
                }
                //brrr reseter
                timer = 0;
            }
        }
    }
}
