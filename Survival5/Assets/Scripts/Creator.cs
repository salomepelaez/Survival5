using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Se importaron los Namespace para poder utilizar sus componentes.
using NPC.Enemy;
using NPC.Ally;

public class Creator : MonoBehaviour
{
    public static bool inGame = true; // Se creó un booleano que controla el estado activo del juego.

    public GameObject thePeople; // Se creó un GameoObject con este nombre, pues será utilizado para Zombies, Ciudadanos y Héroe.
    public GameObject hero;

    System.Random random = new System.Random();

    public const int maxGen = 25; // Se creó una variable constante para el número máximo de generación.
    public readonly int minGen; // Se declaró un readonly para el mínimo posible de generación de objetos. 

    // Las siguientes variables del tipo texto son las que abrigan los contadores del Canvas.
    public TextMeshProUGUI treesNum;
    public TextMeshProUGUI puppetsNum;
    public TextMeshProUGUI villagersNum;
    public string message;
    public static string goMessage;

    public static float sChild; // En esta línea se declara la velocidad estática del héroe, que luego se utiliza en la clase Hero.

    // A continuación en el constructor se asignó el número aleatorio para el mínimo de la creación de objetos.
    public Creator()
    {
        minGen = random.Next(5, 15);
    }

    void Awake()
    {        
        sChild = Random.Range(0.1f, 0.2f);

        int rnd = Random.Range(minGen, maxGen); // La generación es producida entre el mínimo de objetos y el máximo.

        for (int j = 0; j < rnd; j++) // Este For genera los objetos siguiendo los límites establecidos.
        {
            Vector3 pos = new Vector3();
            thePeople = GameObject.CreatePrimitive(PrimitiveType.Cube); 

            pos.x = Random.Range(-50, 50);
            pos.z = Random.Range(-50, 50);
            thePeople.transform.position = pos; // A los cubos se les asigna la posición aleatoria antes mencionada.
                       
            thePeople.AddComponent<Rigidbody>(); // Se les agrega Rigidbody.
            thePeople.GetComponent<Rigidbody>().freezeRotation = true;

            // El siguiente bloque de código se encarga de generar el héroe, está separado, pues a diferencia de los miembros de la aldea, solo debe ser creado una vez.
            if (j == 0)
            {
                thePeople.AddComponent<Child>(); // Se le agregan los componentes de la clase Hero.
                thePeople.AddComponent<ChildAim>(); // Igualmente se le agregan los componentes de HeroAim.
                thePeople.GetComponent<Renderer>().material.color = Color.black; // Se le asignó color negro para diferenciarlos de otros objetos.
            }

            else
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        thePeople.AddComponent<Villagers>(); // Se agregan los componentes de su respectiva clase. 
                        break;
                    case 1:
                        thePeople.AddComponent<Puppet>(); // Se agregan los componentes de su respectiva clase. 
                        break;
                    case 2:
                        thePeople.AddComponent<Trees>(); // Se agregan los componentes de su respectiva clase.
                        break;
                }
            }
        }        
    }

    private void Start()
    {
       goMessage = GameOver(); // Se asignó el mensaje para el momento en el que el jugador pierda.
    }

    private void Update()
    {
        // El siguiente bloque de código genera los contadores de NPC´s en la escena.
        int v = 0;
        int a = 0;
        int p = 0;

        foreach (Trees tree in Transform.FindObjectsOfType<Trees>())
        {
            a = a + 1;
            treesNum.text = "Árboles: " + a;

            if (Creator.inGame == false)
            {
                treesNum.text = "";
            }
        }

        foreach (Puppet puppet in Transform.FindObjectsOfType<Puppet>())
        {
            p = p + 1;
            puppetsNum.text = "Marionetas: " + p;

            if (Creator.inGame == false)
            {
                puppetsNum.text = "";
            }
        }

        foreach (Villagers villagers in Transform.FindObjectsOfType<Villagers>())
        {
            v = v + 1;
            villagersNum.text = "Villagers: " + v;

            if (Creator.inGame == false)
            {
                villagersNum.text = "";
            }
        }
    }

    // Se creó un String que retorna el mensaje, que luego es asignado en el Start.
    public string GameOver()
    {
        message = "Game Over";

        return message;
    }
}
