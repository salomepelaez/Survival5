using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;
   // public GameObject weaponInHand;
    
    public readonly int childAttack = Child.childAttack;
    
    public void Start()
    {
        weapon.SetActive(true);

        if (ChildMove.theWeapon == true)
        {
            weapon.SetActive(false);
        }
    }

    public void Update()
    {
        if(ChildMove.theWeapon == true)
        {
            Child.childAttack = 20;
            
        }
               
    }
       
}

