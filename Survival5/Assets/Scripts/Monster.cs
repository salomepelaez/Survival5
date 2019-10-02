using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC;
using NPC.Ally;

namespace NPC // Este Namespace abriga los otros dos correspondientes: Ally and Enemy
{
    namespace Enemy // Este es el namespace anidado
    {
        public abstract class Monster : NPCConduct
        {
            
            public MonsterData monsterData;
            public Vector3 direction; // Se creó un Vector3 para la dirección.
            public MonsterColor mC;
            
            public void Update()
            {
                GetTarget();

                NPCMove();
            }

            public void GetTarget()
            {
                // El siguiente bloque de código lee la posición de los aldeanos, cuando la distancia es menor al rango, los zombies pasan a perseguirlos.
                Villagers closest = null;
                float closestDistance = 5.0f;

                foreach (var v in FindObjectsOfType<Villagers>())
                {
                    float distance = Vector3.Distance(v.transform.position, transform.position);

                    if (distance < closestDistance)
                    {
                        m = Move.Reacting;
                        closest = v;
                        closestDistance = distance;
                        direction = Vector3.Normalize(v.transform.position - transform.position);
                        transform.position += direction * npcSpeed * Time.deltaTime;
                    }
                }
            }
        }

        sealed class Puppet: Monster
        {
            public static int monsterDamage = 10;

            int health = 100;

            private void Awake()
            {
                transform.tag = "Puppet"; // Se cambió el nombre de la etiqueta.
                transform.name = "Puppet"; // Se cambió el nombre del objeto para poder identificarlo fácilmente.
            }

            public void Start()
            {
                target = FindObjectOfType<Child>().GetComponent<Transform>(); //Se asignó al héroe como target.

                InvokeRepeating("NPCAssignment", 3.0f, 3.0f); // Se llama la repetición para el comportamiento.
                
                mC = MonsterColor.Rojo;
                if (mC == MonsterColor.Rojo)
                {
                    GetComponent<Renderer>().material.color = Color.red;
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
                        m = Move.Reacting;
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

            private void OnTriggerStay(Collider other)
            {
                if (other.transform.tag == "Child")
                {
                    if(Input.GetButtonDown("Fire1"))
                    {
                        health = health - Child.childAttack;
                        Debug.Log(health);

                        if(health <= 0)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }

        sealed class Trees: Monster
        {
            public static int monsterDamage = 15;

            int health = 100;

            private void Awake()
            {
                transform.tag = "Tree"; // Se cambió el nombre de la etiqueta.
                transform.name = "Tree"; // Se cambió el nombre del objeto para poder identificarlo fácilmente.
            }

            public void Start()
            {
                target = FindObjectOfType<Child>().GetComponent<Transform>(); //Se asignó al héroe como target.

                InvokeRepeating("NPCAssignment", 3.0f, 3.0f); // Se llama la repetición para el comportamiento.

                mC = MonsterColor.Verde;
                if (mC == MonsterColor.Verde)
                {
                    GetComponent<Renderer>().material.color = Color.green;
                }
            }

            public override void NPCMove()
            {
                if (Creator.inGame == true) // Solamente cuando el juego está activo el movimiento se genera.
                {
                    attackRange = Vector3.Distance(target.position, transform.position); // El rango de ataque se basa en la distancia con el Target.

                }

                else if (move == "Idle")
                {
                    // ...
                }
                    
                if (attackRange < 5.0f)
                {
                    m = Move.Reacting;
                    direction = Vector3.Normalize(target.transform.position - transform.position);
                    transform.position += direction * npcSpeed * Time.deltaTime;
                }                
            }
            
            // Esta es la función que asigna los estados.
            public override void NPCAssignment()
            {
                switch (Random.Range(0, 6))
                {
                    case 0:
                        m = Move.Idle;
                        move = "Idle";
                        break;
                }
            }

            private void OnTriggerStay(Collider other)
            {
                if (other.transform.tag == "Child")
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        health = health - Child.childAttack;
                        Debug.Log(health);

                        if (health <= 0)
                        {
                            Destroy(this.gameObject);
                            health = 100;
                        }
                    }
                }
            }
        }

        public struct MonsterData // Este Struct almacena todos los datos
        {
            public MonsterColor color;
            public int health;
        }

        public enum MonsterColor // Enum de los colores
        {
            Rojo,
            Verde
        }
    }
}
