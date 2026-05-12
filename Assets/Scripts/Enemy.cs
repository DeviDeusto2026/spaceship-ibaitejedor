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
        if (other.CompareTag("Ammo") && puedeRecibirDano)
        {
            puedeRecibirDano = false;

            vida--;
            Debug.Log("Vida enemiga restante: " + vida);

            Destroy(other.gameObject);

            if (vida <= 0)
            {
                // Buscamos el GameManager
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm != null)
                {
                    gm.EnemigoEliminado(); 
                }

                Destroy(gameObject);
            }
            else
            {
                Invoke("ResetDano", 0.05f);
            }
        }
    }

    void ResetDano()
    {
        puedeRecibirDano = true;
    }
}