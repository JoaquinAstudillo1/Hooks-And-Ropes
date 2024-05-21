using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private List<Respawn> respawnList = new List<Respawn>();

    void Awake()
    {
        // Encontrar todos los objetos con el componente Respawn
        Respawn[] respawns = FindObjectsOfType<Respawn>();
        foreach (Respawn respawn in respawns)
        {
            respawnList.Add(respawn);
        }
    }

    // Activa el nuevo checkpoint
    public void ActivateCheckPoint()
    {
        foreach (Respawn respawn in respawnList)
        {
            respawn.UpdateRespawnPoint(transform);
        }
    }

    // Si el que esta en contacto con el bloque CheckPoint es el Player manda a llamar a la funcion ActiveCheckPoint.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateCheckPoint();
        }
    }
}