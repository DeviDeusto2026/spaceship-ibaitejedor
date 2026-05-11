using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    public int vidas = 3;
    private bool esInvulnerable = false; // Para evitar que una bala te quite las 3 vidas de golpe

    private void OnTriggerEnter(Collider other)
    {
        // Detectamos si lo que nos toca es enemigo o bala de jefe
        if (other.CompareTag("Enemy") || other.CompareTag("BossBullet"))
        {
            if (!esInvulnerable)
            {
                RecibirDano();

                // Si es una bala, la destruimos al impactar
                if (other.CompareTag("BossBullet"))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }

    void RecibirDano()
    {
        vidas--;
        Debug.Log("¡Impacto! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            Debug.Log("¡GAME OVER!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Nos hacemos invulnerables medio segundo para que no nos maten al instante
            esInvulnerable = true;
            Invoke("ResetInvulnerabilidad", 0.5f);
        }
    }

    void ResetInvulnerabilidad()
    {
        esInvulnerable = false;
    }
}
