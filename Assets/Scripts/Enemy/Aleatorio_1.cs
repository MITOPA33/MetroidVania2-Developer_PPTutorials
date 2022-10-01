using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aleatorio_1 : MonoBehaviour
{
    private float velocidad = 2;    //Velocidad de desplazamiento del fantasma

    void Update()
    {
        Debug.Log(velocidad);
        transform.Translate(velocidad * Time.deltaTime, 0, 0);
        if (transform.position.x < 10.7f)   //Rango de desplazamieneto en "+X" (a la Derecha)
            velocidad = -velocidad;
        if (transform.position.x > 5)  //Rango de desplazamieneto en "-X" (a la Izquierda)
            velocidad = -velocidad;
    }
}
