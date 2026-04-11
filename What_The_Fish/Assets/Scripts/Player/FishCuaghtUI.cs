using UnityEngine;
using UnityEngine.UI;

public class FishCuaghtUI : MonoBehaviour
{
    public static FishCuaghtUI instance;

    [SerializeField] private GameObject caughtUI;
    [SerializeField] private GameObject mainUI;
    private Hook hook;

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
            currentTime -= Time.deltaTime;
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
        spawnedFishUI.transform.localPosition = Vector3.zero;
        spawnedFishUI.transform.localScale = Vector3.one * 50f;

        FishCrontroller fishController = spawnedFishUI.GetComponent<FishCrontroller>();
        if (fishController != null) Destroy(fishController);

        originalFish.gameObject.SetActive(false);
    }

    public void OnKeepPressed()
    {
        hook.KeepFish();
        CloseUI();
    }

    public void OnThrowPressed()
    {
        hook.ThrowFish();
        CloseUI();
    }

    private void CloseUI()
    {
        isTimerRunning = false;

        if (spawnedFishUI != null) Destroy(spawnedFishUI);
        if (originalFish != null) originalFish.gameObject.SetActive(true);

        caughtUI.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
    }
}