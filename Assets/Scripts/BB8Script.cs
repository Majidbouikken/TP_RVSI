using UnityEngine;

public class BB8Script : MonoBehaviour
{
    public Transform player; // Reference de PlayerArmature
    public float followRadius = 5f; // Le rayon de suivi de BB8, par default a 3 metres
    public float speed = 0.8f; // Le BB8 est plus lent que le joueur
    public float headRotationSpeed = 5f;
    public float brakes = 3f;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private Rigidbody rb;
    private Transform headTransform;
    private Transform bodyTransform;
    private ParticleSystem poussiere;
    private Transform poussiereTransform;
    private bool isActive = true; // on active ou desactive le BB8 selon ce boolean

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        headTransform = transform.Find("BB8_tete");
        bodyTransform = transform.Find("BB8_corps");
        poussiere = GetComponentInChildren<ParticleSystem>();
        poussiereTransform = transform.Find("Poussiere_fx");
        audioSource = GetComponentInChildren<AudioSource>();

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

        // Son du BB8
        if (Input.GetKeyDown(KeyCode.H))
        {
            MakeSound();
        }

        // Activer ou Desactiver le BB8
        if (Input.GetKeyDown(KeyCode.B))
        {
            Activate();
        }

        // Mouvement du BB8
        if ((distanceToPlayer > followRadius) && isActive)
        {
            MoveBB8(directionToPlayer);
        }
        else if (distanceToPlayer <= followRadius && IsMoving() || !isActive)
        {
            StopBB8();
        }

        // Rotataion de la tete du BB8 et Mouvement de la poussiere
        MoveHead(directionToPlayer, transform.position);
        MovePoussiere(directionToPlayer, transform.position);
    }

    // Cette methode pour gerer l'activation, la vitesse et l'orientation de la poussiere
    public void HandleCollision(Collision collision)
    {
        if (collision.collider.tag == "SolExterieur")
        {
            if (!poussiere.isPlaying)
            {
                poussiere.Play();   
            }
            else if (!IsMoving())
            {
                poussiere.Stop();
            }
            poussiereTransform.LookAt(bodyTransform.position - rb.velocity);
        }
        else
        {
            if (poussiere.isPlaying)
            {
                poussiere.Stop();
            }
        }
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

    void MoveHead(Vector3 directionToPlayer, Vector3 transform)
    {
        // Move head
        Vector3 newHeadPosition = new Vector3(bodyTransform.localPosition.x + transform.x, bodyTransform.localPosition.y + transform.y + 0.27f, bodyTransform.localPosition.z + transform.z);
        headTransform.position = newHeadPosition;

        // Rotate head
        float newHeadAngle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, newHeadAngle, 0f);
        headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, headRotationSpeed * Time.deltaTime);
    }

    void MovePoussiere(Vector3 directionToPlayer, Vector3 transform)
    {
        Vector3 newDustPosition = new Vector3(bodyTransform.localPosition.x + transform.x, bodyTransform.localPosition.y + transform.y + 0.27f, bodyTransform.localPosition.z + transform.z);
        poussiereTransform.position = newDustPosition;
    }

    bool IsMoving()
    {
        return rb.velocity.magnitude > 0.05f;
    }

    public void MakeSound()
    {
        if (!audioSource.isPlaying)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
        }
    }

    public void Activate()
    {
        isActive = !isActive;
    }
}
