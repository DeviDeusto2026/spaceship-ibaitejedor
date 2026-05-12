using UnityEngine;

public class Translation : MonoBehaviour
{
    [Header("Configuración")]
    public Transform centro; // Arrastra aquí el Sol o un objeto vacío en el centro
    public Vector3 ejeOrbita = Vector3.up; // Eje sobre el cual orbita
    public float velocidadOrbita = 20f;

    void Update()
    {
        if (centro != null)
        {
            // Gira alrededor del centro, en el eje elegido, a cierta velocidad
            transform.RotateAround(centro.position, ejeOrbita, velocidadOrbita * Time.deltaTime);
        }
    }
}
