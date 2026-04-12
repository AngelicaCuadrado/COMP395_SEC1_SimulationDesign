using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource sfxSource;
    public AudioSource loopSource;

    [Header("Clips")]
    public AudioClip audioOpenBook;
    public AudioClip audioCloseBook;
    public AudioClip audioPageTurn;
    public AudioClip audioGainPoints;
    public AudioClip audioLosePoints;
    public AudioClip audioFishing;
    public AudioClip audioRelease;

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
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void StartLoop(AudioClip clip)
    {
        if (clip == null) return;
        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.Play();
    }

    public void StopLoop()
    {
        loopSource.Stop();
    }
}