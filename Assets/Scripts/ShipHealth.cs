using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 'other' es el objeto que ha entrado en nuestro espacio
        if (other.CompareTag("Enemy") || other.CompareTag("BossBullet"))
        {
            // El filtro de distancia que pusimos antes sigue siendo buena idea
            float distancia = Vector3.Distance(transform.position, other.transform.position);

            if (distancia < 4f)
            {
                Debug.Log("¡GAME OVER! Chocaste con: " + other.name);
                // Reinicia la escena (asegúrate de que el índice sea el correcto o usa el nombre)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
