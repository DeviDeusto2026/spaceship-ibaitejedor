using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject balaPrefab; 
    public Transform puntoDisparo; // Un objeto vacío en la punta de la nave

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
    }
}
