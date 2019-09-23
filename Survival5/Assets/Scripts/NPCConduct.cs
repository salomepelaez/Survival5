using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;

public class NPCConduct : MonoBehaviour
{
    public float npcSpeed = 2f; // Se asignó una velocidad para los NPCs.
    Vector3 direction; // Se creó un Vector3 para la dirección.
    float attackRange; // Se creó un flotante para el rango de ataque.
    public Transform target; // Se creó un Transform público, que luego es asignado respectivamente en la clase Hero, Villager y Zombie.

    // Este void se encarga del movimiento.
    public void NPCMove()
    {
        if (Creator.inGame == true) // Solamente cuando el juego está activo el movimiento se genera.
        {
            attackRange = Vector3.Distance(target.position, transform.position); // El rango de ataque se basa en la distancia con el Target.
            float rotationSpeed = 25f; // Se creó una variable mucho mayor que la velocidad general del zombie, para que su rotación pueda ser visible.

            if (move == "Moving")
            {
                float rotat = transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0.0f, rotat, 0.0f);
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

    public static Move m;
    string move;

    // Esta es la función que asigna los estados.
    void NPCAssignment()
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

    public enum Move // Enum del movimiento
    {
        Idle,
        Moving,
        Rotating,
        Pursuing,
        Running
    }

    public struct NPCData
    {
        public Move m;
    }
}
