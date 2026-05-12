using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SolarSystemScene");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); 
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Ini");
    }
}
