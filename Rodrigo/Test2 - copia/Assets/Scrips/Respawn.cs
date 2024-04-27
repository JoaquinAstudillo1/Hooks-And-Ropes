using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player;
    public Transform respawnpoint;
    public GrapplingGun grapplingGun; // Añade una referencia al script GrapplingGun

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Respawn del jugador inmediatamente
            RespawnPlayerImmediately();

            // Congelar al jugador durante un segundo
            StartCoroutine(FreezePlayerForDelay());
        }
    }

    void RespawnPlayerImmediately()
    {
        // Respawn inmediato del jugador
        Player.transform.position = respawnpoint.position;
    }

    IEnumerator FreezePlayerForDelay()
    {
        // Congelar al jugador por 1 segundo
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
        yield return new WaitForSeconds(1f);
        
        // Descongelar al jugador después de 1 segundo
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

   
}