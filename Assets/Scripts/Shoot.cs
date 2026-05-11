using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject balaPrefab; 
    public Transform puntoDisparo; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        nuevaBala.transform.forward = transform.forward;
        nuevaBala.transform.Rotate(0, 0, 0);
    }
}
