using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformScript : MonoBehaviour
{

    public float tiempo;
    [SerializeField] private float loop;
    [SerializeField] private float reseteo;
    public GameObject player;
    [SerializeField] private float tiempoReseteo = 30;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Declarar lo que durará el loop del tiempo para la desaparición
        loop = tiempo;
        //Declarar el tiempo de reseteo
        reseteo = tiempoReseteo;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        tiempoReseteo -= Time.deltaTime;

        //Cuando termina el tiempo, la plataforma desaparece
        if (tiempo < 0)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

        }
        //Cuando el tiempo de reseteo llega a su fin, vuelve a aparecer todo el set de plataformas, y vuelta a empezar todo el ciclo
        if(tiempoReseteo < 0)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
           
            tiempo = loop;
            //Resetear el tiempo de reseteo
            tiempoReseteo = reseteo;
        }
        // Si el player cae del mapa, las plataformas vuelven a aparecer y el ciclo se reinicia
        
        if(player.transform.position.y < -4)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;

            tiempo = loop;
            //Resetear el tiempo de reseteo
            tiempoReseteo = reseteo;
        }
       
    }
    
}
