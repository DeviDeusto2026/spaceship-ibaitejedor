using UnityEngine;
using UnityEngine.UI; // Imprescindible para el Slider

public class Boss : MonoBehaviour
{
    [Header("Estadisticas")]
    public int vidaMax = 10;
    private int vidaActual;
    public float velocidadBase = 2f;
    public float frecuenciaZigZag = 2f;
    public float amplitudZigZag = 5f;

    [Header("UI")]
    // Si arrastras el objeto aquí funcionará, si no, lo buscará por nombre
    public GameObject barraVidaObjeto;
    private Slider barraSlider;

    [Header("Ataque")]
    public GameObject balaEnemigaPrefab;
    public Transform puntoDisparo;
    public float tiempoDisparo = 1.5f;

    private Transform jugador;
    private float tiempoLocal;
    private bool puedeRecibirDano = true;

    void Start()
    {
        vidaActual = vidaMax;

        // 1. Buscamos al jugador por Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Ship");
        if (playerObj != null) jugador = playerObj.transform;

        // 2. Lógica para encontrar y activar la barra de vida
        if (barraVidaObjeto == null)
        {
            // Busca en la jerarquía un objeto que se llame exactamente BossHealth
            barraVidaObjeto = GameObject.Find("BossHealth");
        }

        if (barraVidaObjeto != null)
        {
            // Activamos el objeto (por si estaba desactivado al inicio)
            barraVidaObjeto.SetActive(true);

            // Obtenemos el componente Slider para actualizar el valor
            barraSlider = barraVidaObjeto.GetComponent<Slider>();
            if (barraSlider != null)
            {
                barraSlider.maxValue = vidaMax;
                barraSlider.value = vidaMax;
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto BossHealth en la escena.");
        }

        // 3. Iniciamos los disparos
        InvokeRepeating("Disparar", tiempoDisparo, tiempoDisparo);
    }

    void Update()
    {
        if (jugador == null) return;

        tiempoLocal += Time.deltaTime;

        // Movimiento hacia el jugador con zigzag
        Vector3 direccion = (jugador.position - transform.position).normalized;
        Vector3 zigzag = transform.right * Mathf.Sin(tiempoLocal * frecuenciaZigZag) * amplitudZigZag;

        transform.position += (direccion * velocidadBase + zigzag) * Time.deltaTime;

        // El jefe siempre mira al jugador
        transform.LookAt(jugador);
    }

    void Disparar()
    {
        if (balaEnemigaPrefab != null && puntoDisparo != null)
        {
            GameObject bala = Instantiate(balaEnemigaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Usamos linearVelocity (o velocity según versión)
                rb.linearVelocity = puntoDisparo.forward * 15f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta impacto de tu munición
        if (other.CompareTag("Ammo") && puedeRecibirDano)
        {
            puedeRecibirDano = false;
            vidaActual--;

            // Actualizamos la barra visualmente
            if (barraSlider != null) barraSlider.value = vidaActual;

            Destroy(other.gameObject); // Destruye la bala del jugador

            if (vidaActual <= 0)
            {
                // Al morir, desactivamos la barra y destruimos al jefe
                if (barraVidaObjeto != null) barraVidaObjeto.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                // Pequeño tiempo de invulnerabilidad para no morir de un solo impacto múltiple
                Invoke("ResetDano", 0.1f);
            }
        }
    }

    void ResetDano() { puedeRecibirDano = true; }
}