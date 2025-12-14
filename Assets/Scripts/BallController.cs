using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    //Cosas por hacer: 1.Salto / 2.Evitar doble salto / 3.Evitar cambiar de direcci�n en el aire / 4.Respawn de la pelota al salir del suelo / 5.Game Mechanic: TTL(Time to live) Plataforma
    //1) Input del Player
    private PlayerInput input;

    //2) Rigidbody
    private Rigidbody rb;

    //3) Vector 2 para recuperar el input
    private Vector2 InputMovement;
    // Vector que almacena la posici�n inicial para luego poder resetearla en caso de caer del mapa
    private Vector3 posicionInicial;

    private Vector3 direction;

    //Atributos para evitar el doble salto;
    [SerializeField] private bool isGroundedScript = false;

    [Header("Parameters")]
    //Se pone as� para que lo muestre en la interfaz pero el atributo sigue siendo privado
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private float jumpForce = 10;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        //Inicializamos la posici�n inicial
        posicionInicial = transform.position;
        //Evitar que colisione consigo mismo :D
        Physics2D.queriesStartInColliders = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isGroundedScript == true)
        {
            InputMovement = input.actions["Move"].ReadValue<Vector2>();
        }

        direction.x = InputMovement.x;
        direction.z = InputMovement.y;

    }

    private void FixedUpdate()
    {
        if (isGroundedScript)
        {
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }
        
        // Si la bola cae por debajo de -5 en el eje Y (es decir se cae del mapa), vuelve a la posici�n inicial
        if (transform.position.y < -5)
        {
            transform.position = posicionInicial;
        }

        



    }

    void OnCollisionEnter(Collision collision)
    {
        isGroundedScript = true;
        if(collision.gameObject.name == "Trofeo")
        {
            Destroy(collision.gameObject);
        }
        
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        //1.Started
        //2.Perform
        //3.Cancelled

        if (callbackContext.performed && isGroundedScript)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            // (0, 1, 0) * 10 -> (0, 10, 0)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGroundedScript = false;
        }
    }

}
