using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //Agregamos esta librería para cambiar de escena.

public class Contar_1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Ernesto").Length == 0)//Si el fantasma llega a cero..
        {
            SceneManager.LoadScene("SiguienteEscena");                //Cambiamos a "SiguienteEscena"
        }
        else if(GameObject.FindGameObjectsWithTag("Player").Length == 0)//Si el Héroe llega a cero..
        {
            SceneManager.LoadScene("EscenaInicial");                //Cambiamos a "EscenaInicial"
                            //NOTA: el nombre de las escenas debe ser el mismo que en nuestro juego y deben de
                            //estar en "//File/BuildSettings" 
        }
    }
}
