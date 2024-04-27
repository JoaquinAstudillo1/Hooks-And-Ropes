using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimiento : MonoBehaviour
{
    // Start is called before the first frame update
     public Transform player; // Referencia al objeto del jugador

    void Update()
    {
        if (player != null)
        {
            // Actualizar la posición de la cámara para que coincida con la del jugador
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}
