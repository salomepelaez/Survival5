using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Ally;
using NPC.Enemy;
using TMPro;

public class Child : MonoBehaviour
{
    ChildData cs; // Se creó una variable del Struct.
    GameObject pov; // Se creó un GameObject al que se le asignarán los componentes de la cámara. (pov: point of view)
    public readonly float sHero = Creator.sChild; // La variable se asignó como readonly, obteniéndola desde la clase Manager.

    // A continuación se crean las variables de Texto para el Canvas.
    public static Text message;
    public Text gameOver;
    public TextMeshProUGUI health;

    void Start()
    {
        transform.name = "Child"; // Se transformó su nombre para identificarlo más rápidamente.
        transform.tag = "Child";

        // Al GameObject se le asignaron los componentes de cámara, rotación y movimiento.
        GameObject pov = new GameObject();
        pov.AddComponent<Camera>();
        pov.AddComponent<ChildAim>();
        gameObject.AddComponent<ChildMove>();
        gameObject.GetComponent<ChildMove>().speed = sHero; // Se utilizaron los miembros del Enum "Speed", y se reasigna la velocidad.

        // A continuación se asignan lso mensajes directamente al Canvas. 
        message = GameObject.Find("VMessage").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver").GetComponent<Text>();
        health = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        pov.transform.SetParent(this.transform);
        pov.transform.localPosition = Vector3.zero;
    }

    //Rotación en Y.
    public void Update()
    {
        float rotat = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0.0f, rotat, 0.0f);

        health.text = "Salud: " + lifeCounter;  
        if(Creator.inGame == false)
        {
            health.text = "";
        }
    }

    IEnumerator PrintMessages(Villagers villager) // Esta Corutina es la que asigna los mensajes de los ciudadanos.
    {
         message.text = villager.Message();
         yield return new WaitForSeconds(3);
         message.text = "";

        if (Creator.inGame == false)
        {
            message.text = "";
        }
    }

    // La siguiente función es la encargada de imprimir los mensajes cuando hay colisión, utilizando las etiquetas.
    int lifeCounter = 12;
    public void OnCollisionEnter(Collision collision)
    {
       if (collision.transform.tag == "Villager")
       {
            StartCoroutine("PrintMessages", collision.transform.GetComponent<Villagers>());
       }

        if (collision.transform.tag == "Puppet")
        {   
            lifeCounter = lifeCounter - 2;

            if(lifeCounter <= 0)
            {
                Creator.inGame = false;
                gameOver.text = Creator.goMessage; // Igualmente, cuando esto sucede el mensaje de GameOver pasa a ser visible en la escena.
            }            
        }

        if (collision.transform.tag == "Tree")
        {
            lifeCounter = lifeCounter - 4;

            if (lifeCounter <= 0)
            {
                Creator.inGame = false;
                gameOver.text = Creator.goMessage; // Igualmente, cuando esto sucede el mensaje de GameOver pasa a ser visible en la escena.
            }
        }
    }
    
    static float speed; // La velocidad se declaró como estática.
}

public struct ChildData // Este Struct almacena las variables.
{
    public static float sChild;
}

