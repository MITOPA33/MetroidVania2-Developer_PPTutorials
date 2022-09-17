//Script para mover objeto con el teclado izquierda y derecha y para hacer "flip" izquirda derecha.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController_1 : MonoBehaviour
{
    [Header("Animation Variables")]                                                             //"Pesta�a" con t�tulo en el Inspector
    [SerializeField] AnimatorController_1 animatorController;               //Instanciamiento de Clase alias "animatorController"
                                                                            //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.


    [Header("Checker Variables")]                                //Cabecera del ComboBox "Variables"  
    [SerializeField] LayerChecker_1 footA;                  //Instanciamento a la Clase "LayerChecker_1" = footA
    [SerializeField] LayerChecker_1 footB;                  //Instanciamento a la Clase "LayerChecker_1" = footB

    [Header("Boolean Variables")]                           //"Pesta�a" con t�tulo en el Inspector 
    public bool canDoubleJump;                              //variable boleana(verdadero/falso) para ejecutar el salto doble

    [Header("Interruption Variables")]                      //"Pesta�a" con t�tulo en el Inspector              
    public bool canCheckGround;                             //Variable boleana, usada para detectar tocas el piso 


    [Header("Rigid Variables")]
    [SerializeField] private float doubleJumpForce;         //Agregamos una variable flotante para agrear furza al DobleSalto
    [SerializeField] private float speed_;                  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] private Vector2 movementDirection;     //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] private float jumpForce;               //Agregamos una variable flotante para agrear furza al salto



    private Rigidbody2D rigidbody2D_;                       //Variable de instanciamiento
    private bool jumpPressed = false;                       //variable usadas para saber si se apret� la barra espaciadora
                                                            //y es personaje salt�.
    private bool playerIsOnGround;                          //Variable privada tipo Bool, el Heroe esta tocando el piso?




    void Start()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();         //Variable de instanciamiento tipo "RigidBody2D"
        animatorController.Play(AnimationId.Idle);                               //Llamamos a la clase "AnimatorController_1" y le mandamos a su
                                                                                 //M�todo "Play" el "string" "Idle" guardado en "AnimationId"

    }

    // Update is called once per frame
    void Update()
    {
        HandleIsGrounding();                                 //Invoca al m�todo "HandleIsGrounding" (El h�roe est� tocando el piso?). 
        HandleControls();                                    //invocando el m�todo "HandleControls" (abre el puerto de entrada del teclado)
        HandleMovement();                                    //invocando el m�todo "HandleMovement" (multiplica el valor de "x" por "speed".
        HandleFlip();                                       //invocando el m�todo "HandleFlip" (rota el personaje a la izquierda o a la derecha)
        HandleJump();                                       //invocando el m�todo "HandleJump" (agregamos furza vertical hacia arriba al Hero)

    }

    void HandleIsGrounding()
    {
        playerIsOnGround = footA.isTouching || footB.isTouching;	//Las variables de InstanciaDeClase footAB invocan a la ariable "isTouching" 
    }                                                                   //de la clase "LayerChecker_1", si ambas son verdaderas entonces
                                                                        //"playerIsOnGround"

    void HandleControls()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");          //asociamos la variable "jumpPressed" a la barra espaciadora


    }

    void HandleMovement()
    {
        rigidbody2D_.velocity = new Vector2(movementDirection.x * speed_, rigidbody2D_.velocity.y);
        if (playerIsOnGround)                           //Si la variable "playerIsOnGround" es verdadera entonces...
        { 
            if (Mathf.Abs(rigidbody2D_.velocity.x) > 0)    	//si el velor absoluto matem�tico de la velocidasd es mayor a cero..
        {
            animatorController.Play(AnimationId.Run);    //ejecuta en el script AnimatorController_1 el m�todo "Play"
                                                         //mandando dentro de la variable "AnimationAI" el clip "Run"
        }
        else                                                //de otro modo ejecuta en el script AnimatorController_1 el m�todo "Play"
                                                            //mandando dentro de la variable "AnimationAI" el clip "Idle"
        {
            animatorController.Play(AnimationId.Idle);
        }
        }

    }
    void HandleFlip()
    {
        if (rigidbody2D_.velocity.magnitude > 0)                //S�lo si el personaje se est� moviendo ejecuta estas lineas...
        {
            if (rigidbody2D_.velocity.x >= 0)                           //si la velocidad en "x" es mayor que cero ejecuta la siguiente linea....
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);            //No rotes
            }
            else                                                                            //de otro modo ejecuta las siguientes lineas.....
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);              //rota en "y" 180�
            }
        }
    }
    void HandleJump()           //M�todo para agregarle fuerza la RigidBody del Hero
    {
        if (canDoubleJump && jumpPressed && !playerIsOnGround)  //"!playerIsOnGround" esta variable nos indica que NO esta tocando el piso
        {
            this.rigidbody2D_.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);//agrega impulso de fuerza instant�nea hacia arriba al doble salto           
            canDoubleJump = false;                                                        //apagamos la variable "canDoubleJump� para que no brinque infinitamente
        }


        if (jumpPressed && playerIsOnGround)        //si la barra espaciadora es apretada y el H�roe toca el piso


        {
            this.rigidbody2D_.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);//agrega impulso de fuerza instant�nea hacia arriba           
            StartCoroutine(HandleJumpAnimation());                                  //Invocamos Corrutina "HandleJumpAnimation"El H�roe brinca, pero no 						cambia el clip a �Jump�
            canDoubleJump = true;                                                   //prendemos la variable "canDoubleJump" para que brinque
                                                                                    //de nuevo si apretamos la barra espaciadora 


        }
    }

    IEnumerator HandleJumpAnimation()                       //Corrutina que ejecuta dos "clips" desfasados en tiempo 0.4f unidades de tiempo
    {
        yield return new WaitForSeconds(0.1f);                  //Espera 0.1f uniades de tiempo
        animatorController.Play(AnimationId.PrepararBrinco);   //Ejecuta el clip "PrepararBrinco"
        yield return new WaitForSeconds(0.2f);                  //Espera 0.2f uniades de tiempo
        animatorController.Play(AnimationId.Brincar);           //Ejecuta el clip "Brinco"
   

    }

}