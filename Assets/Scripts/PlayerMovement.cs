using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement sharedInstance;

    //ESTABLECER LAS VARIABLES
    private Vector3 startPosition; //Establece la posición del personaje
    public float jumpForce = 6.0f; //Establecer la fuerza del salto
    public float runningSpeed = 1.0f; //Establecer la velocidad del conejo

    private Rigidbody2D myRigidbody2d; //Declarar una variable de la clase Rigidbody2D
    public Animator myAnimator; //Declarar una variable de la clase Animator
    public LayerMask groundLayerMask; //Guarda la variable de acceso directo a una capa llamada "ground"

    void Awake()
    {
        sharedInstance = this;
        startPosition = this.transform.position;
        myRigidbody2d = GetComponent<Rigidbody2D>(); //Obtener el componente de la clase Rigidbody2D para acceder a el y 
        myAnimator.SetBool("isAlive", true);
    }


    public void StartGame()
    {
        //Cuando empiece el juego, la animación del personaje empezará con que el personaje está vivo.
        myAnimator.SetBool("isAlive", true);
        this.transform.position = startPosition;
        myRigidbody2d.velocity = new Vector2(0, 0);
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //Programar el salto al presionar el boton izquierdo del raton
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
            myAnimator.SetBool("isGrounded", isGrounded()); //Comprobar la animación cada frame si esta en el suelo
    }

    //Unity llama cada intervalo de tiempo fijo. Mejor método para añadir fuerzas constantes a la física, como el movimiento horizontal
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            if (myRigidbody2d.velocity.x < runningSpeed)
            {
                myRigidbody2d.velocity = new Vector2(runningSpeed, myRigidbody2d.velocity.y);
            }
        }
        
    }

    //Crear el método salto del personaje
    void Jump()
    {
        //Si el personaje se encuentra en el suelo, se le permite usar el salto
        if (isGrounded())
        {
           
            //Acceder a la propiedad ADDFORCE
            myRigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //La longitud del vector hacia arriba (1) * la fuerza deL salto * El impulso de la clase ForceMode2D
        }

    }

    //Crear el método de retorno para comprobar que el personaje esta en el suelo
    bool isGrounded()
    {

        /*Si el objeto suelo (groundLayerMask.value) está a una distancia inferior a 1.0f por debajo (Vector2.down) 
        del conejo (this.transform.position), devolver true. En caso contrario, devolver false. */
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value)) 
            //El personaje lanza un rayo hacia abajo a una distancia determinada hacia una capa de suelo
        {
            return true;
        }
        else return false;
    }

    public void KillPlayer()
    {
        myAnimator.SetBool("isAlive", false);
        GameManager.sharedInstance.GameOver();
        
    }
}
