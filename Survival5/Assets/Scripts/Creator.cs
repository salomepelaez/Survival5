﻿using System.Collections;
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

    GameObject thePeople; // Se creó un GameoObject con este nombre, pues será utilizado para Zombies, Ciudadanos y Héroe.

    System.Random random = new System.Random();

    public const int maxGen = 25; // Se creó una variable constante para el número máximo de generación.
    public readonly int minGen; // Se declaró un readonly para el mínimo posible de generación de objetos. 

    // Las siguientes variables del tipo texto son las que abrigan los contadores del Canvas.
    public TextMeshProUGUI monsterNum;
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
            thePeople = GameObject.CreatePrimitive(PrimitiveType.Cube); // El GameObject "thePeople" genera los cubos para zombies, aldeanos y héroes.

            // El Vector3 de posición es el que servirá para generar los cubos en una posición aleatoria.
            Vector3 posicion = new Vector3();
            posicion.x = Random.Range(-50, 50);
            posicion.z = Random.Range(-50, 50);
            thePeople.transform.position = posicion; // A los cubos se les asigna la posición aleatoria antes mencionada.

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
        int z = 0;

        foreach (Monster monter in Transform.FindObjectsOfType<Monster>())
        {
            z = z + 1;
            monsterNum.text = "Monstruos: " + z;
        }

        foreach (Villagers villagers in Transform.FindObjectsOfType<Villagers>())
        {
            v = v + 1;
            villagersNum.text = "Villagers: " + v;
        }
    }

    // Se creó un String que retorna el mensaje, que luego es asignado en el Start.
    public string GameOver()
    {
        message = "Game Over";

        return message;
    }
}
