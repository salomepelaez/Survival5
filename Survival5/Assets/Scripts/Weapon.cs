using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject stick;
    Vector3 direction;

    public void Awake()
    {
        transform.tag = "Weapon";
        Vector3 posicion = new Vector3();
        posicion.x = -20;
        posicion.y = -0.5f;
        posicion.z = 20;

        GameObject s = Instantiate(stick, posicion, Quaternion.identity);
        s.GetComponent<Renderer>().material.color = Color.cyan;
        s.AddComponent<Rigidbody>();
    }

}

