using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))] // Esto asegura que siempre haya un AudioSource
public class VideoPlaylist : MonoBehaviour
{
    public VideoClip[] clips;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        // Configuración de Audio por código
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.controlledAudioTrackCount = 1;

        videoPlayer.loopPointReached += OnClipEnd;

        if (clips.Length > 0)
        {
            videoPlayer.clip = clips[0];
            videoPlayer.Play();
        }
    }

    void OnClipEnd(VideoPlayer vp)
    {
        if (clips.Length == 0) return;

        currentIndex = (currentIndex + 1) % clips.Length;
        vp.clip = clips[currentIndex];

        // Al cambiar de clip, a veces Unity necesita que le reconfirmemos el audio
        vp.SetTargetAudioSource(0, audioSource);
        vp.Play();
    }
}