using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Ally;
using NPC.Enemy;
using TMPro;

public class Child : MonoBehaviour
{
    public static int childAttack = 1; // El daño de ataque es mínimo porque es un niño desarmado.
    public GameObject weaponInHand; // A este GameObject se le asigna el prefab del hacha en la mano.

    // A continuación se crean las variables de Texto para el Canvas.
    public static Text message;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI health;
    public Text objectsMessage;
    public TextMeshProUGUI isArmed;    

    // Los siguientes booleanos son de protección y defensa, correspondientemente.
    public static bool unbreakable = false;
    public bool armed = false;

    void Awake()
    {
        transform.name = "Child"; // Se transformó su nombre para identificarlo más rápidamente.
        transform.tag = "Child";

        Initialize();
    }

    // En la inicialización se asignan los mensajes al canvas.
    private void Initialize()
    {
        message = GameObject.Find("VMessage").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
        objectsMessage = GameObject.Find("Objects").GetComponent<Text>();
        health = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        isArmed = GameObject.Find("Armed").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        weaponInHand.SetActive(false); // El arma en la mano se ha puesto como falsa, pues de primera mano no es necesario que se vea.       

        if (Creator.inGame == true) // Cuando el juego recién comienza, el texto de estado del arma es el siguiente:
        {
            isArmed.text = "Desarmado";
        }
    }

    public void Update()
    {     
        health.text = "Salud: " + lifeCounter; // El contador de salud es el encargado de mostrar cuánta vida le queda al héroe.
        
        if(Creator.inGame == false)
        {
            health.text = "";
        }

        if (armed == true) // Cuando el héroe pasa a estar armado, el daño de ataque aumenta.
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

    // La siguiente función es la encargada de registrar la colisión y el daño.
    int lifeCounter = 100; // El héroe posee la misma vida que los enemigos, si el contador llega a 0, el héroe muere.
    
    public void OnCollisionEnter(Collision collision)
    {
       if (collision.transform.tag == "Villager")
       {
            StartCoroutine("PrintMessages", collision.transform.GetComponent<Villagers>());
       }

        // Cuando el héroe registra una colisión con un enemigo, pasa a perder vida.
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
                gameOver.text = Creator.goMessage; 
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
                gameOver.text = Creator.goMessage; 
            }            
        }
    }

    // El siguiente OnTriggerStay permite recoger objetos en la escena.
    // El arma brinda defensa.
    // La manta brinda protección.

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

        if (other.transform.tag == "Teddy")
        {
            objectsMessage.text = "Presiona E para recoger el osito";

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
    int childAttack;
}

