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

    GameObject thePeople;

    // Las siguientes variables del tipo texto son las que abrigan los contadores del Canvas.
    public TextMeshProUGUI treesNum;
    public TextMeshProUGUI puppetsNum;
    public TextMeshProUGUI villagersNum;

    // Las siguientes variables almacenarán los mensajes del UI.
    public string message;
    public static string goMessage;
    public static string w;
    public TextMeshProUGUI theW;

    private void Start()
    {
        Initialize(); // Se llama la función para inicializar.
        goMessage = GameOver(); // Se asignó el mensaje para el momento en el que el jugador pierda.  
    }

    private void Initialize()
    {
        int minGen = Random.Range(5, 26);
        theW = GameObject.Find("Winner").GetComponent<TextMeshProUGUI>(); // Se asignó el mensaje del ganador al Canvas.

        for (int j = 0; j < minGen; j++) // Este For genera los objetos siguiendo los límites establecidos.
        {
            Vector3 pos = new Vector3();
            thePeople = GameObject.CreatePrimitive(PrimitiveType.Cube);

            pos.x = Random.Range(-50, 50);
            pos.z = Random.Range(-50, 50);
            thePeople.transform.position = pos; // A los cubos se les asigna una posición aleatoria.

            thePeople.AddComponent<Rigidbody>(); // Se les agrega Rigidbody.
            thePeople.GetComponent<Rigidbody>().freezeRotation = true;

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

    bool bigWinner; // Con este bool se activará el texto en caso de ganar.

    private void Update()
    {
        Counter(); // Se llamó a la función que hace el conteo de NPCs en la escena

        if(bigWinner == true)
        {
            theW.text = Winner(); 
        }
    }

    private void Counter() // Esta función contea y proyecta la cantidad de NPCs en un Canvas.
    {
        int v = 0;
        int a = 0;
        int p = 0;

        foreach (Trees tree in Transform.FindObjectsOfType<Trees>())
        {
            a = a + 1;
            treesNum.text = "Trees: " + a;            
        }

        if (a == 0)
        {
            treesNum.text = "";
        }

        foreach (Puppet puppet in Transform.FindObjectsOfType<Puppet>())
        {
            p = p + 1;
            puppetsNum.text = "Puppets: " + p;
        }

        if (p == 0)
        {
            puppetsNum.text = "";
        }

        foreach (Villagers villagers in Transform.FindObjectsOfType<Villagers>())
        {
            v = v + 1;
            villagersNum.text = "Villagers: " + v;

            if (inGame == false)
            {
                villagersNum.text = "";
            }
        }

        if (a ==0  && p == 0) // Si los contadores de enemigos llegan a 0, el booleano de ganador pasa a ser verdadero y activar un texto.
        {
            inGame = false;
            bigWinner = true;
        }
    }

    // Se crearon dos Strings que retornan los mensajes, que luego son asignados en otra función.
    public string GameOver()
    {
        message = "Game Over";
        return message;
    }

    public string Winner()
    {
        message = "Winner!";
        return message;
    }
}
