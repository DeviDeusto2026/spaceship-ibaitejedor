using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float velocidadBala = 50f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidadBala * Time.deltaTime);

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


            Destroy(gameObject);

        }
    }
}
