using UnityEngine;
using UnityEngine.UI;

public class FishCuaghtUI : MonoBehaviour
{
    public static FishCuaghtUI instance;

    [SerializeField] private GameObject caughtUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private Hook hook;

    [Header("Visual Elements")]
    [SerializeField] private Image timerBar;
    [SerializeField] private Transform fishContainer;

    [Header("Time Settings")]
    [SerializeField] private float timeLimit = 5f;

    private float currentTime;
    private bool isTimerRunning = false;
    private GameObject spawnedFishUI;
    private Fish originalFish;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.unscaledDeltaTime;
            timerBar.fillAmount = currentTime / timeLimit;

            if (currentTime <= 0)
            {
                OnThrowPressed();
            }
        }
    }

    public void ShowCaughtUI(Hook hook, Fish fish)
    {
        this.hook = hook;
        this.originalFish = fish;

        if (mainUI != null) mainUI.SetActive(false);
        caughtUI.SetActive(true);

        currentTime = timeLimit;
        timerBar.fillAmount = 1f;
        isTimerRunning = true;

        if (spawnedFishUI != null) Destroy(spawnedFishUI);

        spawnedFishUI = Instantiate(fish.gameObject, fishContainer);
        spawnedFishUI.transform.localPosition = new Vector3(120f, -30f, 0f);
        spawnedFishUI.transform.localScale = originalFish.transform.localScale * 900f;
        spawnedFishUI.transform.localRotation = Quaternion.Euler(-90f, 90f, 0f);
        SetLayerRecursively(spawnedFishUI, LayerMask.NameToLayer("UI"));

        FishCrontroller fishController = spawnedFishUI.GetComponent<FishCrontroller>();
        if (fishController != null) Destroy(fishController);

        Animator anim = spawnedFishUI.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.speed = 5f;
        }

        AudioManager.Instance.StartLoop(AudioManager.Instance.audioFishing);

        originalFish.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void OnKeepPressed()
    {
        AudioManager.Instance.StopLoop();

        if (originalFish.fishData.isGoodFish)
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioGainPoints);
        else
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioLosePoints);

        hook.KeepFish();
        CloseUI();
    }

    public void OnThrowPressed()
    {
        AudioManager.Instance.StopLoop();
        AudioManager.Instance.PlaySound(AudioManager.Instance.audioRelease);
        hook.ThrowFish();
        CloseUI();
    }

    private void CloseUI()
    {
        isTimerRunning = false;
        caughtUI.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
        if (spawnedFishUI != null) Destroy(spawnedFishUI);

        Time.timeScale = 1f;
    }
}