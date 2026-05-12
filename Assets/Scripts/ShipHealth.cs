using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    public int vidas = 3;
    public GameObject[] corazones;
    private bool esInvulnerable = false; 

    private void OnCollisionEnter(Collision other)
    {

       if( other.gameObject.CompareTag("Ammo") || other.gameObject.CompareTag("BossBullet"))
        {
            return;
        }
        RecibirDano();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Este mensaje saldrá CUALQUIER COSA que toque la nave
        Debug.Log("Algo ha tocado la nave: " + other.gameObject.name + " con Tag: " + other.tag);

        if (other.CompareTag("BossBullet"))
        {
            RecibirDano();
            Destroy(other.gameObject);
        }
    }

    void RecibirDano()
    {
        vidas--;
        ActualizarCorazones();
        //Debug.Log("¡Impacto! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            Debug.Log("¡GAME OVER!");
            SceneManager.LoadScene("Inicio");
        }
        else
        {
            esInvulnerable = true;
            Invoke("ResetInvulnerabilidad", 0.5f);
        }
    }

    void ActualizarCorazones()
    {
        if (vidas >= 0 && vidas < corazones.Length)
        {
            corazones[vidas].SetActive(false);
        }
    }
    void ResetInvulnerabilidad()
    {
        esInvulnerable = false;
    }
}
