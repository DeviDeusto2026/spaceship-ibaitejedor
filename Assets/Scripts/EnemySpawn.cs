using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float tiempoEntreSpawn = 3f;
    public float radioSpawn = 50f;

    void Start()
    {
        InvokeRepeating("Spawn", 2f, tiempoEntreSpawn);
    }

    void Spawn()
    {
        Vector3 posAleatoria = Random.insideUnitSphere * radioSpawn;
        posAleatoria.y = 0; 

        Instantiate(enemigoPrefab, posAleatoria, Quaternion.identity);
    }

    public void DetenerSpawn()
    {
        CancelInvoke("Spawn");
    }
}
