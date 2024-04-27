using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    private float maxDistance = 15f;
    private SpringJoint joint;
    private bool isGrappling = false;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isGrappling)
        {
            StartGrapple(Quaternion.Euler(0f, 30f, 0f) * Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.W) && isGrappling)
        {
            StopGrapple();
        }

        if (Input.GetKeyDown(KeyCode.D) && !isGrappling)
        {
            StartGrapple(Quaternion.Euler(0f, 0f, 45f) * Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.D) && isGrappling)
        {
            StopGrapple();
        }

        if (Input.GetKeyDown(KeyCode.A) && !isGrappling)
        {
            StartGrapple(Quaternion.Euler(0f, 0f, -45f) * Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.A) && isGrappling)
        {
            StopGrapple();
        }
        if (Input.GetKeyDown(KeyCode.X) && !isGrappling)
        {
            StartGrapple(Quaternion.Euler(0f, 0f, 45f) * Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.X) && isGrappling)
        {
            StopGrapple();
        }
        if (Input.GetKeyDown(KeyCode.Z) && !isGrappling)
        {
            StartGrapple(Quaternion.Euler(0f, 0f, -45f) * Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isGrappling)
        {
            StopGrapple();
        }

    
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
        // Calculate the direction from player to grapple point
        Vector3 grappleDirection = (grapplePoint - player.position).normalized;

        // Apply force to the player in the direction of input
        player.GetComponent<Rigidbody>().AddForce(horizontalInput * player.right, ForceMode.Acceleration);
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

        lr.positionCount = 2;
        isGrappling = true;
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
        //If not grappling, don't draw rope
        if (!isGrappling) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void ResetGrapple()
{
    if (isGrappling)
    {
        StopGrapple();
    }
}
}