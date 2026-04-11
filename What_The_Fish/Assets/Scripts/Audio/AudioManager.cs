using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Player")]
    public AudioSource sfxSource;

    [Header("Your Sound Effects (Drag the files here)")]
    public AudioClip audioOpenBook;
    public AudioClip audioCloseBook;
    public AudioClip audioPageTurn;
    public AudioClip audioGainPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}