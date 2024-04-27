using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public LayerMask obstacleLayer; // Capa que representa los obstáculos

    
    void Update()
    {
        // Obtener las entradas del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.up;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right;
        }
        
       
        

        moveDirection.Normalize(); // Normalizar la dirección para evitar movimientos diagonales más rápidos

        // Mover el objeto en la dirección calculada
         
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, moveDirection, out hit, 0.35f, obstacleLayer))
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
