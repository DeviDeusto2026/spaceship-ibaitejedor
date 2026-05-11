using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidad = 10f;
    public int vida = 3;
    private Transform jugador;
    private bool puedeRecibirDano = true; 


    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Ship");
        if (playerObj != null) jugador = playerObj.transform;
    }

    void Update()
    {
        if (jugador != null)
        {
            transform.LookAt(jugador);
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo restamos vida si es una bala Y si el enemigo "está listo" para recibir daño
        if (other.CompareTag("Ammo") && puedeRecibirDano)
        {
            puedeRecibirDano = false; // Cerramos la puerta

            vida--;
            Debug.Log("Vida enemiga restante: " + vida);

            Destroy(other.gameObject); // Destruye la bala

            if (vida <= 0)
            {
                FindObjectOfType<GameManager>().EnemigoEliminado();
                Destroy(gameObject);
            }
            else
            {
                // Espera un suspiro (0.05 segundos) y vuelve a permitir daño
                Invoke("ResetDano", 0.05f);
            }
        }
    }

    void ResetDano()
    {
        puedeRecibirDano = true;
    }
}


