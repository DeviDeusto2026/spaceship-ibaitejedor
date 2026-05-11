using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Estad�sticas")]
    public int vida = 10;
    public float velocidadBase = 2f; 
    public float frecuenciaZigZag = 2f;
    public float amplitudZigZag = 5f;

    [Header("Ataque")]
    public GameObject balaEnemigaPrefab;
    public Transform puntoDisparo;
    public float tiempoDisparo = 1.5f;

    [Header("Spawner")]
    public GameObject miniNavePrefab;

    private Transform jugador;
    private float tiempoLocal;
    private bool puedeRecibirDano = true;

    void Start()
    {
        // Buscamos al jugador por el Tag que usaste antes
        GameObject playerObj = GameObject.FindGameObjectWithTag("Ship");
        if (playerObj != null) jugador = playerObj.transform;

        // CORRECCI�N: El nombre debe coincidir exactamente con el m�todo
        InvokeRepeating("Disparar", tiempoDisparo, tiempoDisparo);

        // Detenemos el spawn de enemigos peque�os
        if (FindObjectOfType<EnemySpawn>() != null)
            FindObjectOfType<EnemySpawn>().DetenerSpawn();
    }

    void Update()
    {
        if (jugador == null) return;

        tiempoLocal += Time.deltaTime;

        // Movimiento hacia el jugador con zigzag
        Vector3 direccion = (jugador.position - transform.position).normalized;
        Vector3 zigzag = transform.right * Mathf.Sin(tiempoLocal * frecuenciaZigZag) * amplitudZigZag;

        transform.position += (direccion * velocidadBase + zigzag) * Time.deltaTime;

        // El jefe siempre te mira
        transform.LookAt(jugador);
    }

    void Disparar()
    {
        if (balaEnemigaPrefab != null && puntoDisparo != null)
        {
            // 1. Creamos la bala
            GameObject bala = Instantiate(balaEnemigaPrefab, puntoDisparo.position, puntoDisparo.rotation);

            // 2. Le damos fuerza si tiene Rigidbody
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = puntoDisparo.forward * 15f; // 15 es la velocidad
            }
        }
    }

    // CORRECCI�N: Usamos Trigger porque as� configuramos el juego antes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo") && puedeRecibirDano)
        {
            puedeRecibirDano = false;
            vida--;
            Debug.Log("Vida del Jefe: " + vida);

            Destroy(other.gameObject); // Destruye tu bala

            // Suelta una mini nave de refuerzo
            if (miniNavePrefab != null)
                Instantiate(miniNavePrefab, transform.position + Vector3.back * 2f, Quaternion.identity);

            if (vida <= 0)
            {
                Debug.Log("�JEFE DERROTADO!");
                // Aqu� podr�as llamar a una pantalla de victoria
                Destroy(gameObject);
            }
            else
            {
                Invoke("ResetDano", 0.1f);
            }
        }
    }

    void ResetDano() { puedeRecibirDano = true; }
}