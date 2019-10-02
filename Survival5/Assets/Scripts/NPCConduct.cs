using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;

public abstract class NPCConduct : MonoBehaviour
{
    public float npcSpeed = 2f; // Se asignó una velocidad para los NPCs.
    Vector3 direction; // Se creó un Vector3 para la dirección.
    public float attackRange; // Se creó un flotante para el rango de ataque.
    public Transform target; // Se creó un Transform público, que luego es asignado respectivamente en la clase Hero, Villager y Zombie.

    // Este void se encarga del movimiento.
    public abstract void NPCMove();   

    public static Move m;
    public string move;

    public abstract void NPCAssignment();

    public enum Move // Enum del movimiento
    {
        Idle,
        Moving,
        Rotating,
        Reacting
    }

    public struct NPCData
    {
        public Move m;
    }
}
