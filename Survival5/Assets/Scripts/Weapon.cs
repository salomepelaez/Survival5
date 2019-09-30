using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;
    public GameObject weaponInHand;

    Vector3 direction;

    public void Awake()
    {
        transform.tag = "Weapon";
        Vector3 post = new Vector3();
        post.x = -20;
        post.y = -0.5f;
        post.z = 20;

        GameObject s = Instantiate(weapon, post, Quaternion.identity);
        s.AddComponent<Rigidbody>();
    }

    public void Update()
    {
        if(Child.armed == true)
        {
            Child.childAttack = 10;
        }
    }    
}

