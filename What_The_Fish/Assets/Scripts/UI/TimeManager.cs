using System.Collections;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI daysLeftText;

    [Header("Level")]
    [SerializeField] public int levelNumber = 1;
    [SerializeField] private string[] levelNames = { "Saugatuck", "Allegan Dam", "Kalamazoo City" };
    [SerializeField] private TextMeshProUGUI levelLabelText;

    [Header("Zone Banner")]
    [SerializeField] private GameObject nextLevelUI;
    [SerializeField] private TextMeshProUGUI zoneText;
    [SerializeField] private float bannerDuration = 2f;

    [Header("Settings")]
    [SerializeField] private int totalDays = 3;
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private int startHour = 8;
    [SerializeField] private int endHour = 20;

    [Header("Zone Max Stats (per zone)")]
    [SerializeField] private int[] maxFoodPerZone    = { 300, 450, 500 };
    [SerializeField] private int[] maxMercuryPerZone = { 300, 450, 500 };

    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerEmotionManager emotionManager;

    [Header("Scoring")]
    [SerializeField] private float mercurySafeRatio = 0.10f;
    [SerializeField] EndGameUIManager ui;

    [Header("Level Moving")]
    [SerializeField] private GameObject playerPrefab;

    private float currentTimeInMinutes;
    private int currentDay = 1;
    private bool isTimerRunning = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (levelNumber == 1)
            SaveManager.ClearSave();

        UpdateLevelLabel();
        StartDay();
    }

    void Update()
    {
        if (isTimerRunning)
            UpdateTime();
    }

    private void StartDay()
    {
        currentTimeInMinutes = startHour * 60;
        isTimerRunning = true;
        Time.timeScale = 1f;
        UpdateVisuals();
    }

    private void UpdateTime()
    {
        currentTimeInMinutes += Time.deltaTime * timeMultiplier;
        UpdateVisuals();

        if (currentTimeInMinutes >= endHour * 60)
            EndDay();
    }

    private void UpdateVisuals()
    {
        int hours   = Mathf.FloorToInt(currentTimeInMinutes / 60);
        int minutes = Mathf.FloorToInt(currentTimeInMinutes % 60);

        string amPm     = hours >= 12 ? "PM" : "AM";
        int displayHour = hours > 12 ? hours - 12 : hours;
        if (displayHour == 0) displayHour = 12;

        if (timeText != null)
            timeText.text = string.Format("{0:00}:{1:00} {2}", displayHour, minutes, amPm);

        int daysRemaining = (totalDays - currentDay) + 1;
        if (daysLeftText != null)
            daysLeftText.text = daysRemaining + (daysRemaining == 1 ? " Day Left" : " Days Left");
    }

    private void UpdateLevelLabel()
    {
        if (levelLabelText != null)
            levelLabelText.text = "Level " + levelNumber + ": " + GetLevelName();
    }

    private string GetLevelName()
    {
        if (levelNames != null && levelNumber - 1 < levelNames.Length)
            return levelNames[levelNumber - 1];
        return "";
    }

    private void EndDay()
    {
        isTimerRunning = false;
        currentDay++;

        if (currentDay <= totalDays)
        {
            StartDay();
        }
        else
        {
            StartCoroutine(EndZoneCoroutine(0));
        }
    }

    // Called by Cache when food or mercury hits max
    public void EndZoneEarly(bool foodFull)
    {
        if (!isTimerRunning) return;
        isTimerRunning = false;
        int bonus = foodFull ? 200 : -200;
        StartCoroutine(EndZoneCoroutine(bonus));
    }

    // Called by the END GAME button in the top-right corner
    public void TriggerEndGame()
    {
        if (!isTimerRunning) return;
        isTimerRunning = false;
        StartCoroutine(EndZoneCoroutine(0));
    }

    private IEnumerator EndZoneCoroutine(int bonusPoints)
    {
        Time.timeScale = 0f;

        SaveLevelStats(bonusPoints);

        if (levelNumber < 3)
        {
            levelNumber++;
            Debug.Log("Next Level");
            // Move player and camera to new level positions
            Transform playerSpawn = LevelPositionManager.instance.GetPlayerSpawn(levelNumber);
            Transform cameraPos = LevelPositionManager.instance.GetCameraPosition(levelNumber);

            if (playerSpawn != null)
            {
                playerPrefab.transform.position = playerSpawn.position;
                playerPrefab.transform.rotation = playerSpawn.rotation;
            }

            CameraMover cam = Camera.main.GetComponent<CameraMover>();
            if (cam != null && cameraPos != null)
                cam.MoveTo(cameraPos);

            // Reset cache for the new zone
            int zoneIndex = levelNumber - 1;
            int newMaxFood    = zoneIndex < maxFoodPerZone.Length    ? maxFoodPerZone[zoneIndex]    : 300;
            int newMaxMercury = zoneIndex < maxMercuryPerZone.Length ? maxMercuryPerZone[zoneIndex] : 300;

            if (player != null)
                player.cache.ResetForNewZone(newMaxFood, newMaxMercury);

            // Reset day counter for the new zone
            currentDay = 1;

            UpdateLevelLabel();

            // Show zone banner
            if (zoneText != null)
                zoneText.text = levelNumber.ToString();

            if (nextLevelUI != null)
                nextLevelUI.SetActive(true);

            yield return new WaitForSecondsRealtime(bannerDuration);

            if (nextLevelUI != null)
                nextLevelUI.SetActive(false);

            StartDay();
        }
        else
        {
            if (ui != null)
            {
                ui.gameObject.SetActive(true);
                int survivors = SaveManager.GetCumulativeScore();
                int stars = GameOverManager.ScoreToStars(survivors);
                ui.ShowResults(survivors, stars);
            }
            //PauseManager pauseManager = FindFirstObjectByType<PauseManager>();
            //pauseManager?.ShowGameOver(true);
        }
    }

    private void SaveLevelStats(int bonusPoints)
    {
        if (player == null) return;

        int food = player.cache.food;
        int mercury = player.cache.mercury;
        int maxFood = player.cache.maxFood;
        int maxMercury = player.cache.maxMercury;

        //Scoring System
        const int villagers = 100;
        const float foodPerVillager = 1f;

        //Starvation
        int fed = Mathf.Min(villagers, Mathf.FloorToInt(food / foodPerVillager));
        int starved = villagers - fed;

        //Mercury Poisoning
        float ratio = (float)mercury / Mathf.Max(food, 1);
        int poisoned = 0;

        if (ratio >= mercurySafeRatio)
        {
            float lethalRatio = ratio - mercurySafeRatio;
            poisoned = Mathf.Clamp(Mathf.FloorToInt(fed * lethalRatio), 0, fed);
        }

        //Final survivors (0–100 score)
        int survivors = Mathf.Clamp(fed - poisoned, 0, villagers);

        //Apply bonus points
        //survivors = Mathf.Clamp(survivors + bonusPoints, 0, villagers);

        //Convert survivors to stars
        int stars = 0;
        if (survivors >= 67) stars = 3;
        else if (survivors >= 34) stars = 2;
        else if (survivors >= 1) stars = 1;

        //Save stars and stats
        SaveManager.SaveLevelStars(levelNumber, stars);
        SaveManager.SaveLevelStats(levelNumber, food, maxFood, mercury, maxMercury, survivors);
        SaveManager.AddToCumulativeScore(survivors);
    }
}
