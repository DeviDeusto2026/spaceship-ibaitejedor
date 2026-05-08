using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidad = 10f;
    public int vida = 3;
    private Transform jugador;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            vida--;
            Destroy(collision.gameObject); 

            if (vida <= 0)
            {
                Destroy(gameObject); 
            }
        }

        if (vida <= 0)
        {
            // Buscamos al GameManager y le sumamos uno al contador
            FindObjectOfType<GameManager>().EnemigoEliminado();
            Destroy(gameObject);
        }
    }
}
