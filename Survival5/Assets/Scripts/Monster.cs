using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC;
using NPC.Ally;

namespace NPC // Este Namespace abriga los otros dos correspondientes: Ally and Enemy
{
    namespace Enemy // Este es el namespace anidado
    {
        public class Monster : NPCConduct
        {
            public MonsterData monsterData;
            Vector3 direction; // Se creó un Vector3 para la dirección.
            MonsterColor mC;

            public void Start()
            {
                target = FindObjectOfType<Child>().GetComponent<Transform>(); //Se asignó al héroe como target.

                InvokeRepeating("NPCAssignment", 3.0f, 3.0f); // Se llama la repetición para el comportamiento.
                transform.tag = "Monster"; // Se cambió el nombre de la etiqueta.
                transform.name = "Monster"; // Se cambió el nombre del objeto para poder identificarlo fácilmente.

                mC = MonsterColor.Rojo;
                if (mC == MonsterColor.Rojo)
                {
                    GetComponent<Renderer>().material.color = Color.red;
                }
            }

            private void Update()
            {
                NPCMove();

                // El siguiente bloque de código lee la posición de los aldeanos, cuando la distancia es menor al rango, los zombies pasan a perseguirlos.
                Villagers closest = null;
                float closestDistance = 5.0f;

                foreach (var v in FindObjectsOfType<Villagers>())
                {
                    float distance = Vector3.Distance(v.transform.position, transform.position);

                    if (distance < closestDistance)
                    {
                        m = Move.Pursuing;
                        closest = v;
                        closestDistance = distance;
                        direction = Vector3.Normalize(v.transform.position - transform.position);
                        transform.position += direction * npcSpeed * Time.deltaTime;
                    }
                }
            }
        }

        public struct MonsterData // Este Struct almacena todos los datos
        {
            public MonsterColor mC;
            public int age;
        }

        public enum MonsterColor // Enum de los colores
        {
            Rojo
        }
    }
}
