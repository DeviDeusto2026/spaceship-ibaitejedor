using UnityEngine;

public class Translation : MonoBehaviour
{
    [Header("Configuración")]
    public Transform centro; 
    public Vector3 ejeOrbita = Vector3.up; 
    public float velocidadOrbita = 20f;

    void Update()
    {
        if (centro != null)
        {
            transform.RotateAround(centro.position, ejeOrbita, velocidadOrbita * Time.deltaTime);
        }
    }
}
