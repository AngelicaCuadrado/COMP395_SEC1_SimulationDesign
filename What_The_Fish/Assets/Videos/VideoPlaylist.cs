using UnityEngine;
using UnityEngine.Video;

public class VideoPlaylist : MonoBehaviour
{
    public VideoClip[] clips;  // assign your 2 clips in the Inspector
    private VideoPlayer videoPlayer;
    private int currentIndex = 0;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnClipEnd;
        videoPlayer.clip = clips[0];
        videoPlayer.Play();
    }

    void OnClipEnd(VideoPlayer vp)
    {
        currentIndex = (currentIndex + 1) % clips.Length;
        vp.clip = clips[currentIndex];
        vp.Play();
    }
}
