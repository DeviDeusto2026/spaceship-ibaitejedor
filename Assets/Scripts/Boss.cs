using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Estadísticas")]
    public int vida = 10;
    public float velocidadBase = 5f;
    public float frecuenciaZigZag = 2f;
    public float amplitudZigZag = 5f;

    [Header("Ataque")]
    public GameObject balaEnemigaPrefab;
    public Transform puntoDisparo;
    public float tiempoDisparo = 2f;

    [Header("Spawner")]
    public GameObject miniNavePrefab;

    private Transform jugador;
    private float tiempoLocal;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Ship").transform;
        
        InvokeRepeating("Shoot", tiempoDisparo, tiempoDisparo);

        FindObjectOfType<EnemySpawn>().DetenerSpawn();
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
        Instantiate(balaEnemigaPrefab, puntoDisparo.position, puntoDisparo.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo")) 
        {
            vida--;
            Destroy(collision.gameObject);

            
            Instantiate(miniNavePrefab, transform.position + Vector3.up, Quaternion.identity);

            if (vida <= 0)
            {
                Debug.Log("¡JEFE DERROTADO!");
                Destroy(gameObject);
            }
        }
    }
}
