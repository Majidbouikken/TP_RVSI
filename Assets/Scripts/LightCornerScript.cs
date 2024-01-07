using UnityEngine;

public class LightCornerScript : MonoBehaviour
{
    private Light light; // On prend la reference de la lumiere

    public float minDistance = 3f;
    public float maxDistance = 6f;
    public float intensityMultiplier = 6f;
    public Transform player;

    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlyaer = Vector3.Distance(transform.position, player.position);
            distanceToPlyaer = Mathf.Clamp(distanceToPlyaer, minDistance, maxDistance);
            float inversedDistance = Mathf.InverseLerp(maxDistance, minDistance, distanceToPlyaer);
            light.intensity = inversedDistance * intensityMultiplier;
        }
    }
}
