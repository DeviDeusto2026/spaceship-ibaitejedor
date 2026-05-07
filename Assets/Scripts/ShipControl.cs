using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public float velocidad = 20f;
    public float sensibilidadRotacion = 5f;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        float movHorizontal = Input.GetAxis("Horizontal"); // A/D
        float movVertical = Input.GetAxis("Vertical");     // W/S

        Vector3 direccion = new Vector3(movHorizontal, movVertical, 0);
        rb.MovePosition(transform.position + direccion * velocidad * Time.deltaTime);

        //Pantalla
        //float xLimitado = Mathf.Clamp(transform.position.x, -10f, 10f);
        //float yLimitado = Mathf.Clamp(transform.position.y, -5f, 5f);
        //transform.position = new Vector3(xLimitado, yLimitado, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("¡CRASH! Nave destruida.");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
