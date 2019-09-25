using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Se importó el Namespace para poder utilizar sus componentes.
using NPC;
using NPC.Enemy;

namespace NPC // Este Namespace abriga los otros dos correspondientes: Ally and Enemy
{
    namespace Ally // Este es el namespace anidado
    {
        public class Villagers : NPCConduct
        {
            public VillagersData villagersData; // Se creó una variable del Struct.

            Vector3 direction;

            void Start()
            {
                target = FindObjectOfType<Monster>().GetComponent<Transform>(); // Para el Ciudadano el objetivo es el Zombie.

                transform.tag = "Villager"; // El cambiar el nombre de la etiqueta, permite encontrar de manera sencilla el objeto con el que se colisiona.
                transform.name = "Villager"; // Se cambió el nombre del objeto para poder identificarlo fácilmente.

                GetComponent<Renderer>().material.color = Color.yellow; // Se añadió el color amarillo a los aldeanos para poder distinguirlos de los zombies.

                // A continuación se añadieron las variables del tipo Random para la edad y nombre.
                // Este bloque de código se realizó en el Start, porque de esta manera se asignan las variables solo una vez por objeto creado.

                villagersData.age = Random.Range(15, 101);

                InvokeRepeating("NPCAssignment", 3.0f, 3.0f); // Se llama la repetición para el comportamiento.

                npcSpeed = (15f * npcSpeed) / villagersData.age; // Esta regla de tres inversa se encarga de asignar una velocidad, dependiendo de la edad.                              
            }

            private void Update()
            {
                NPCMove();

                // El siguiente bloque de código lee la posición de los zombies, cuando la distancia es menor al rango, los ciudadanos pasan a huir.
                Monster closest = null;
                float closestDistance = 5.0f;

                foreach (var v in FindObjectsOfType<Monster>())
                {
                    float distance = Vector3.Distance(v.transform.position, transform.position);

                    if (distance < closestDistance)
                    {
                        m = Move.Reacting;
                        closest = v;
                        closestDistance = distance;
                        direction = Vector3.Normalize(v.transform.position - transform.position);
                        transform.position -= direction * npcSpeed * Time.deltaTime;
                    }
                }
            }

            public override void NPCMove()
            {
                if (Creator.inGame == true) // Solamente cuando el juego está activo el movimiento se genera.
                {
                    attackRange = Vector3.Distance(target.position, transform.position); // El rango de ataque se basa en la distancia con el Target.
                    float rotationSpeed = 25f; // Se creó una variable mucho mayor que la velocidad general del zombie, para que su rotación pueda ser visible.

                    if (move == "Moving")
                    {
                        transform.position += transform.forward * npcSpeed * Time.deltaTime;
                    }

                    else if (move == "Idle")
                    {
                        // ...
                    }

                    else if (move == "Rotating")
                    {
                        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    }

                    if (attackRange < 5.0f)
                    {
                        direction = Vector3.Normalize(target.transform.position - transform.position);
                        transform.position += direction * npcSpeed * Time.deltaTime;
                    }
                }
            }

            // Esta es la función que asigna los estados.
            public override void NPCAssignment()
            {
                switch (Random.Range(0, 6))
                {
                    case 0:
                        m = Move.Moving;
                        move = "Moving";
                        break;

                    case 1:
                        m = Move.Idle;
                        move = "Idle";
                        break;

                    case 2:
                        m = Move.Rotating;
                        move = "Rotating";
                        break;
                }
            }

            public string Message()
            {
                string myMessage = "";

                switch(Random.Range(0,6))
                {
                    case 0:
                        myMessage = "No debes ir al bosque sin adulto";
                        break;
                    case 1:
                        myMessage = "Cuenta la leyenda que los árboles tenían vida, y luchaban por los humanos, pero se rindieron al ver que su peor enemigo eran ellos mismos.";
                        break;
                    case 2:
                        myMessage = "Las marionetas son más que simples juguetes, debes tener cuidado";
                        break;
                    case 3:
                        myMessage = "¿Has luchado contra árboles? Pero qué personita más valiente";
                        break;
                    case 4:
                        myMessage = "Buenardo";
                        break;
                    case 5:
                        myMessage = "Busca una manera de protegerte";
                        break;

                }

                return myMessage;
            }

            public void OnCollisionEnter(Collision collision)
            {
                if (collision.transform.tag == "Tree")
                {
                    gameObject.AddComponent<Trees>().monsterData = (MonsterData)GetComponent<Villagers>().villagersData;
                    Destroy(gameObject.GetComponent<Villagers>());
                }

                if (collision.transform.tag == "Puppet")
                {
                    gameObject.AddComponent<Puppet>().monsterData = (MonsterData)GetComponent<Villagers>().villagersData;
                    Destroy(gameObject.GetComponent<Villagers>());
                }
            }
        }


        public struct VillagersData // Este Struct almacena las variables.
        {
            public int age;

            // En este pequeño bloque se realiza el cast de estructuras.
            public static explicit operator MonsterData(VillagersData vD)
            {
                MonsterData zD = new MonsterData();
                zD.age = vD.age;

                return zD;
            }
        }
    }
}
