using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("¡GAME OVER!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
