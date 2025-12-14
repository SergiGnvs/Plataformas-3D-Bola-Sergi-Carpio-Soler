using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Atributos de la clase

    // Vector que almacena la posición inicial para luego poder resetearla en caso de caer del mapa
    private Vector3 posicionInicial;
    // Componente PlayerInput, gestión del input de Unity
    PlayerInput input;
    // Vector que almacenará el movimiento obtenido del input
    private Vector2 movement;

    // Vector director del movimiento en el espacio 3D
    private Vector3 direction;

    private Rigidbody rb;

    [Header("Parameters")]
    //Se pone así para que lo muestre en la interfaz pero el atributo sigue siendo privado
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Inicializamos la posición inicial
        posicionInicial = transform.position;
        // Obtenemos el componente
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // Leer los valores que hemos puesto en el Input Actions de Unity
        movement = input.actions["Move"].ReadValue<Vector2>();

        //Pasar los valores de vector en 2D a 3D 
        // Movimiento con respecto del mundo
        direction = new Vector3(movement.x, 0f, movement.y);

        // Movimiento con respecto del objeto
        direction = transform.TransformDirection(direction);

        // Se multiplica por Time.deltaTime para que el movimiento sea independiente de la tasa de frames
        transform.position += direction * speed * Time.deltaTime; 

        // Imprime "Movement" por pantalla
        //Debug.Log("Movement");


    }

    void FixedUpdate()
    {
        // Si la bola cae por debajo de -5 en el eje Y (es decir se cae del mapa), vuelve a la posición inicial
        if(transform.position.y < 5)
        {
            transform.position = posicionInicial;
        }

        if (Input.GetAxis("Jump") > 0)
        {
            //rb.linearVelocityY = 0f;
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpForce);
            //isGroundedScript = false;

        }
    }

}
