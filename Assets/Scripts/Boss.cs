using UnityEngine;
using UnityEngine.UI; 

public class Boss : MonoBehaviour
{
    [Header("Estadisticas")]
    public int vidaMax = 10;
    private int vidaActual;
    public float velocidadBase = 2f;
    public float frecuenciaZigZag = 2f;
    public float amplitudZigZag = 5f;

    [Header("UI")]
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

        GameObject playerObj = GameObject.FindGameObjectWithTag("Ship");
        if (playerObj != null) jugador = playerObj.transform;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform barraTransform = canvas.transform.Find("BossHealth");
            if (barraTransform != null)
            {
                barraVidaObjeto = barraTransform.gameObject;
                barraVidaObjeto.SetActive(true); 

                barraSlider = barraVidaObjeto.GetComponent<Slider>();
                if (barraSlider != null)
                {
                    barraSlider.maxValue = vidaMax;
                    barraSlider.value = vidaMax;
                }
            }
        }

        InvokeRepeating("Disparar", tiempoDisparo, tiempoDisparo);
    }

    void Update()
    {
        if (jugador == null) return;

        tiempoLocal += Time.deltaTime;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        Vector3 zigzag = transform.right * Mathf.Sin(tiempoLocal * frecuenciaZigZag) * amplitudZigZag;

        transform.position += (direccion * velocidadBase + zigzag) * Time.deltaTime;

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
                rb.linearVelocity = puntoDisparo.forward * 15f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo") && puedeRecibirDano)
        {
            puedeRecibirDano = false;
            vidaActual--;

            if (barraSlider != null) barraSlider.value = vidaActual;

            Destroy(other.gameObject); 
            if (vidaActual <= 0)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm != null) gm.EnemigoEliminado();

                if (barraVidaObjeto != null) barraVidaObjeto.SetActive(false);
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