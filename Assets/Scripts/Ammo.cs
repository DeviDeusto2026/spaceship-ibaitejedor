using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float velocidadBala = 50f;
    public GameObject efectoImpacto; 

    void Update()
    {
        transform.position += transform.forward * velocidadBala * Time.deltaTime;

        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ship"))
        {
            return;
        }


        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log("¡Bala impactó en un planeta!");

            if (efectoImpacto != null)
            {
                Instantiate(efectoImpacto, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);

        }
    }
}
