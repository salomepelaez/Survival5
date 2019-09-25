using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject stick;

    public void Start()
    {
        Vector3 posicion = new Vector3();
        posicion.x = -20;
        posicion.y = -0.5f;
        posicion.z = 20;

        GameObject s = Instantiate(stick, posicion, Quaternion.identity);
        s.GetComponent<Renderer>().material.color = Color.cyan;
           
        if (Input.GetKey(KeyCode.E))
        {
            Destroy(gameObject.GetComponent<Weapon>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Child")
        {
            message.text = "Presiona E para recoger";
        }
    }
}

