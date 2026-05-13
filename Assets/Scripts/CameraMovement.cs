using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public int movSpeed = 30;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false; 
        }
    }
    void Update()
    {
        move();
    }

    private void move()
    {
        bool hayInput = false;

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position -= transform.right * Time.deltaTime * movSpeed;
        }

        else if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += transform.forward * Time.deltaTime * movSpeed;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position -= transform.forward * Time.deltaTime * movSpeed;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += transform.right * Time.deltaTime * movSpeed;
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.position += Vector3.up * Time.deltaTime * movSpeed;
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            gameObject.transform.position += Vector3.down * Time.deltaTime * movSpeed;
        }

        if (!hayInput && rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log("¡IMPACTO DETECTADO! Game Over");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   



    }
}