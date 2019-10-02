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
    public  static int childAttack = 1;
    public GameObject weaponInHand;

    // A continuación se crean las variables de Texto para el Canvas.
    public static Text message;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI health;
    public Text objectsMessage;
    public TextMeshProUGUI isArmed;
    public TextMeshProUGUI theW;
    public static bool unbreakable = false;
    public bool armed = false;

    void Awake()
    {
        transform.name = "Child"; // Se transformó su nombre para identificarlo más rápidamente.
        transform.tag = "Child";

        // A continuación se asignan lso mensajes directamente al Canvas. 
        Initialize();
    }

    /// <summary>
    /// Initilizes required components
    /// </summary>
    private void Initialize()
    {
        message = GameObject.Find("VMessage").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
        objectsMessage = GameObject.Find("Objects").GetComponent<Text>();
        health = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        isArmed = GameObject.Find("Armed").GetComponent<TextMeshProUGUI>();
        theW = GameObject.Find("Winner").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        weaponInHand.SetActive(false);
        isArmed.text = "Desarmado";

        if (Creator.inGame == false)
        {
            isArmed.text = "";
        }

        if (Creator.bigWinner == true)
        {
            theW.text = Creator.w;
        }
    }

    //Rotación en Y.
    public void Update()
    {     
        health.text = "Salud: " + lifeCounter;  
        if(Creator.inGame == false)
        {
            health.text = "";
        }

        if (armed == true)
        {
            childAttack = 20;
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
    int lifeCounter = 100;
    
    public void OnCollisionEnter(Collision collision)
    {
       if (collision.transform.tag == "Villager")
       {
            StartCoroutine("PrintMessages", collision.transform.GetComponent<Villagers>());
       }

        if (collision.transform.tag == "Puppet")
        {   
            lifeCounter = lifeCounter - Puppet.monsterDamage;
            
            if(unbreakable == true)
            {
                lifeCounter = lifeCounter - (Puppet.monsterDamage/2);
            }

            if (lifeCounter <= 0)
            {
                Creator.inGame = false;
                gameOver.text = Creator.goMessage; // Igualmente, cuando esto sucede el mensaje de GameOver pasa a ser visible en la escena.
            }  
        }

        if (collision.transform.tag == "Tree")
        {
            lifeCounter = lifeCounter - Trees.monsterDamage;

            if (unbreakable == true)
            {
                lifeCounter = lifeCounter - (Trees.monsterDamage/2);
            }

            if (lifeCounter <= 0)
            {
                Creator.inGame = false;
                gameOver.text = Creator.goMessage; // Igualmente, cuando esto sucede el mensaje de GameOver pasa a ser visible en la escena.
            }            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Weapon")
        {
            objectsMessage.text = "Presiona E para recoger el hacha";

            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.gameObject.SetActive(false);
                isArmed.text = "Armado";
                weaponInHand.SetActive(true);
                objectsMessage.text = "";

                armed = true;
                Debug.Log(armed);
            }
        }

        if (other.transform.tag == "Blanket")
        {
            objectsMessage.text = "Presiona E para recoger la manta";

            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.gameObject.SetActive(false);
                objectsMessage.text = "";

                unbreakable = true;
                Debug.Log("mamadísimo");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Weapon")
        {
            objectsMessage.text = "";
        }
    }
}

public struct ChildData // Este Struct almacena las variables.
{
    float sChild;
}

