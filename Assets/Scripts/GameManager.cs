using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [Header("Configuración del Jefe")]
    public int enemigosDerrotados = 0;
    public int metaParaJefe = 10;
    public GameObject jefePrefab;
    public Transform puntoSpawnJefe;
    private bool jefeHaSalido = false;

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoKillsUI; 
    public static int killsFinales; 

    void Start()
    {
        ActualizarInterfaz();
    }

    public void EnemigoEliminado()
    {
        enemigosDerrotados++;
        killsFinales = enemigosDerrotados; 

        ActualizarInterfaz();

        if (enemigosDerrotados >= metaParaJefe && !jefeHaSalido)
        {
            SpawnearJefe();
        }
    }

    void SpawnearJefe()
    {
        jefeHaSalido = true;

        EnemySpawn spawner = FindObjectOfType<EnemySpawn>();
        if (spawner != null) spawner.DetenerSpawn();

        if (jefePrefab != null && puntoSpawnJefe != null)
        {
            Instantiate(jefePrefab, puntoSpawnJefe.position, puntoSpawnJefe.rotation);
        }

        Debug.Log("¡ALERTA: EL JEFE HA LLEGADO!");
    }

    void ActualizarInterfaz()
    {
        if (textoKillsUI != null)
        {
            textoKillsUI.text = "Kills: " + enemigosDerrotados;
        }
    }
}
