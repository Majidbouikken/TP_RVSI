using UnityEngine;

public class BB8Script : MonoBehaviour
{
    public Transform player; // Reference de PlayerArmature
    public float followRadius = 3f; // Le rayon de suivi de BB8, par default a 3 metres
    public float speed = 0.8f; // Le BB8 est plus lent que le joueur
    public float headRotationSpeed = 5f;
    public float brakes = 3f;

    private bool isActive = true; // on active ou desactive le BB8 selon ce boolean
    private Rigidbody rb;
    private Transform headTransform;
    private Transform bodyTransform;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        headTransform = transform.Find("BB8_tete");
        bodyTransform = transform.Find("BB8_corps");
        audioSource = GetComponent<AudioSource>();

        if (rb == null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(bodyTransform.position, player.position);
        Vector3 directionToPlayer = (player.position - bodyTransform.position);

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            isActive = !isActive;
        }
        if ((distanceToPlayer > followRadius) && isActive)
        {
            MoveBB8(directionToPlayer);
        }
        else if (distanceToPlayer <= followRadius && IsMoving() || !isActive)
        {
            StopBB8();
        }
        MoveHead(directionToPlayer);
    }

    void MoveBB8(Vector3 directionToPlayer)
    {
        directionToPlayer.y = 0;
        directionToPlayer.Normalize();

        rb.AddForce(directionToPlayer * speed, ForceMode.Acceleration);
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

    void MoveHead(Vector3 directionToPlayer)
    {
        Vector3 newHeadPosition = new Vector3(bodyTransform.localPosition.x, bodyTransform.localPosition.y + 0.27f, bodyTransform.localPosition.z);
        headTransform.position = newHeadPosition;

        // Rotate head
        float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
        headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, headRotationSpeed * Time.deltaTime);
    }

    bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }
}
