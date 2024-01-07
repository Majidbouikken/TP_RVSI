using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public float triggerDistance = 3f;
    public Transform player;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < triggerDistance && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
        else if (distanceToPlayer > triggerDistance && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}
