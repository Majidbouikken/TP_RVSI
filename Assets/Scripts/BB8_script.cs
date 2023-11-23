using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB8_script : MonoBehaviour
{
    public Transform player; // Reference de PlayerArmature
    public float followRadius = 3f; // Le rayon de suivi de BB8, par default a 3 metres
    public float speed = 1.5f;
    public float brakes = 3f;
    public float gravity = 9.8f;

    private Rigidbody rb;
    private Transform headTransform;
    private Transform bodyTransform;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        headTransform = transform.Find("BB8_tete");
        bodyTransform = transform.Find("BB8_corps");

        if (rb == null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > followRadius)
        {
            MoveBB8();
        }
        else if (distanceToPlayer <= followRadius && IsMoving())
        {
            StopBB8();
        }
    }

    void MoveBB8()
    {
        Vector3 acceleration = (player.position - transform.position);
        acceleration.y = 0;
        acceleration.Normalize();

        rb.AddForce(acceleration * speed, ForceMode.Acceleration);
    }

    void StopBB8()
    {
        Vector3 deceleration = -rb.velocity.normalized;

        rb.AddForce(deceleration * brakes, ForceMode.Acceleration);

        if (!IsMoving())
        {
            rb.velocity = Vector3.zero;
        }
    }

    bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }
}
