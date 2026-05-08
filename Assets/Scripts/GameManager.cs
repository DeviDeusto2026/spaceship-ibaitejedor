using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemigosDerrotados = 0;
    public int metaParaJefe = 10; 
    public GameObject jefePrefab;
    public Transform puntoSpawnJefe;

    private bool jefeHaSalido = false;

    
    public void EnemigoEliminado()
    {
        enemigosDerrotados++;

        if (enemigosDerrotados >= metaParaJefe && !jefeHaSalido)
        {
            SpawnearJefe();
        }
    }

    void SpawnearJefe()
    {
        jefeHaSalido = true;

        FindObjectOfType<EnemySpawn>().DetenerSpawn();

        Instantiate(jefePrefab, puntoSpawnJefe.position, puntoSpawnJefe.rotation);

        Debug.Log("¡ALERTA: EL JEFE HA LLEGADO!");
    }
}
