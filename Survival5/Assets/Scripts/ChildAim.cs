using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAim : MonoBehaviour
{
    public bool InvertedMouse;
    // Las variables del mouse se asignaron como flotantes pues también necesitan almacenar la parte decimal.
    float mouseX;
    float mouseY;
    float sensitivity = 40.0f; // Se agregó una variable extra para la sensibilidad de la cámara.
    float axisLimit = 0.0f; // También se agregó una variable para el límite de rotación de la cámara.

    // El siguiente bloque de código se encarga de generar la rotación de la cámara en los ejes X y Y. 
    void Update()
    {
        if (Creator.inGame == true)
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            axisLimit = mouseY;

            if (InvertedMouse)
            {
                mouseY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            }

            else
            {
                mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            }

            /* 
            * Adicional a esto, utilizando la variable para el límite, se procedió a indicar cuál es el máximo de rotación
            * permitida para un jugador, evitando así que este pueda girar complemtamente sobre sí mismo, y se pierda 
            * la utilidad de la cámara.
            */

            if (axisLimit > 90.0f)
            {
                axisLimit = 90.0f;
                mouseY = 90.0f;
            }

            else if (axisLimit < -90.0f)
            {
                axisLimit = -90.0f;
                mouseY = -90.0f;
            }

            transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        }
    }
}
