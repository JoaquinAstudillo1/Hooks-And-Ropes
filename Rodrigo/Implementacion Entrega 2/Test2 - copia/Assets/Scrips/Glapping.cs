using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    private float maxDistance = 10f;
    private SpringJoint joint;
    private bool isGrappling = false;

    private KeyCode currentActiveKey = KeyCode.None;
    private bool isActionInProgress = false;

    private bool shouldDrawRope = true;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        HandleGrappleInput(KeyCode.W, Quaternion.Euler(0f, 30f, 0f) * Vector3.up);
        HandleGrappleInput(KeyCode.D, Quaternion.Euler(0f, 0f, 45f) * Vector3.right);
        HandleGrappleInput(KeyCode.A, Quaternion.Euler(0f, 0f, -45f) * Vector3.left);
        HandleGrappleInput(KeyCode.X, Quaternion.Euler(0f, 0f, 45f) * Vector3.down);
        HandleGrappleInput(KeyCode.Z, Quaternion.Euler(0f, 0f, -45f) * Vector3.down);

        HandleHorizontalMovement();
    }

   void HandleGrappleInput(KeyCode key, Vector3 direction)
{
    if (Input.GetKey(key))
    {
        if (!isGrappling && currentActiveKey == KeyCode.None && !isActionInProgress)
        {
            isActionInProgress = true;
            currentActiveKey = key;
            
            // Verificar si el objeto al que se apunta es grappleable
            RaycastHit hit;
            Vector3 raycastOrigin = player.position;
            if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance, whatIsGrappleable))
            {
                StartGrapple(direction);
            }
            else
            {
                Debug.Log("No hay un objeto grappleable en la dirección a la que estás apuntando.");
                isActionInProgress = false;
                currentActiveKey = KeyCode.None;
            }
        }
    }
    else if (Input.GetKeyUp(key))
    {
        if (isGrappling && currentActiveKey == key)
        {
            StopGrapple();
            currentActiveKey = KeyCode.None;
            isActionInProgress = false;
        }
    }
}

    void HandleHorizontalMovement()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        // Only allow movement if grappling
        if (isGrappling)
        {
            // Apply force to the player in the direction of input
            player.GetComponent<Rigidbody>().AddForce(horizontalInput * player.right, ForceMode.Acceleration);
        }
        else if (!isGrappling)
        {
            // Aplica fuerza lateral para movimiento natural de izquierda a derecha
            float naturalMovementForce = 1f; // Puedes ajustar este valor según sea necesario
            player.GetComponent<Rigidbody>().AddForce(Vector3.right * naturalMovementForce * horizontalInput, ForceMode.Acceleration);
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple(Vector3 rayDirection)
{   
    RaycastHit hit;
    
    Vector3 raycastOrigin = new Vector3(player.position.x, player.position.y, player.position.z); // Origin is slightly above the player
    
    Debug.DrawRay(raycastOrigin, rayDirection * maxDistance, Color.red);

    if (Physics.Raycast(raycastOrigin, rayDirection, out hit, maxDistance, whatIsGrappleable))
    {
        if(hit.transform.gameObject.CompareTag("imposible")) // Comprueba si el objeto golpeado tiene el tag "escenario"
        {
            Debug.Log("El objeto golpeado no tiene el tag 'escenario', no se traza la cuerda."); 
            
        }

        else
        {
            grapplePoint = hit.point;

            Debug.Log("distancia:" + hit.distance);
            Debug.Log("Punto de impacto:" + hit.point);
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;


            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // Adjust these values to fit your game.
            joint.maxDistance = distanceFromPoint * 0.30f;
            joint.minDistance = distanceFromPoint * 0.25f;
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2; // Dibujar la cuerda
            shouldDrawRope = true; // Dibujar la cuerda
            isGrappling = true;
        }
        
    }
     else
    {
        Debug.DrawLine(raycastOrigin, raycastOrigin + rayDirection * maxDistance, Color.green);

    }
}

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        isGrappling = false;
    }

   void DrawRope()
{
    // Si no estamos agarrando o no debemos dibujar la cuerda, no dibujar la cuerda
    if (!isGrappling || !shouldDrawRope) return;

    lr.SetPosition(0, gunTip.position);
    lr.SetPosition(1, grapplePoint);
}

    public void ResetGrapple()
    {
        if (isGrappling)
        {
            StopGrapple();
            currentActiveKey = KeyCode.None;
            isActionInProgress = false;
        }
    }

    
}