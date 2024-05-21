using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimiento : MonoBehaviour
{
    public Transform player; // Referencia al objeto del jugador
    public float offsetX = 1f; // Mitad del ancho del rectángulo imaginario
    public float offsetY = 0.5f; // Mitad de la altura del rectángulo imaginario
    public float smoothSpeed = 0.125f; // Velocidad de suavizado de la cámara

    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    void Start()
    {
        // Asegúrate de que la cámara comience centrada en el jugador
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        Vector3 playerPosition = player.position;

        float cameraPosX = transform.position.x;
        float cameraPosY = transform.position.y;

        // Para el eje X
        if (playerPosition.x > cameraPosX + offsetX)
        {
            cameraPosX = playerPosition.x - offsetX;
        }
        else if (playerPosition.x < cameraPosX - offsetX)
        {
            cameraPosX = playerPosition.x + offsetX;
        }

        // Para el eje Y
        if (playerPosition.y > cameraPosY + offsetY)
        {
            cameraPosY = playerPosition.y - offsetY;
        }
        else if (playerPosition.y < cameraPosY - offsetY)
        {
            cameraPosY = playerPosition.y + offsetY;
        }

        desiredPosition = new Vector3(cameraPosX, cameraPosY + 1f, transform.position.z);

        // Suavizar la posición de la cámara
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}