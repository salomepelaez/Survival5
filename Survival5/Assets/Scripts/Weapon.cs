using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public TextMeshProUGUI message;

    public void Start()
    {
        message = GameObject.Find("Weapon").GetComponent<TextMeshProUGUI>();
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Child")
        {
            message.text = "Presiona E para recoger";
        }
    }
}

