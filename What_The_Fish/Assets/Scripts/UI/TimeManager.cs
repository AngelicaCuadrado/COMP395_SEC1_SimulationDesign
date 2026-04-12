using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI daysLeftText;
    [SerializeField] private GameObject dayOverUI;

    [Header("Summary Stats")]
    [SerializeField] private TextMeshProUGUI foodScoreText;
    [SerializeField] private TextMeshProUGUI mercuryScoreText;
    [SerializeField] private PlayerController player;

    [Header("Settings")]
    [SerializeField] private int totalDays = 3;
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private int startHour = 8;
    [SerializeField] private int endHour = 20;

    private float currentTimeInMinutes;
    private int currentDay = 1;
    private bool isTimerRunning = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartDay();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            UpdateTime();
        }
    }

    private void StartDay()
    {
        currentTimeInMinutes = startHour * 60;
        isTimerRunning = true;
        Time.timeScale = 1f;
        dayOverUI.SetActive(false);
        UpdateVisuals();
    }

    private void UpdateTime()
    {
        currentTimeInMinutes += Time.deltaTime * timeMultiplier;

        UpdateVisuals();

        if (currentTimeInMinutes >= endHour * 60)
        {
            EndDay();
        }
    }

    private void UpdateVisuals()
    {
        int hours = Mathf.FloorToInt(currentTimeInMinutes / 60);
        int minutes = Mathf.FloorToInt(currentTimeInMinutes % 60);

        string amPm = hours >= 12 ? "PM" : "AM";
        int displayHour = hours > 12 ? hours - 12 : hours;
        if (displayHour == 0) displayHour = 12;

        timeText.text = string.Format("{0:00}:{1:00} {2}", displayHour, minutes, amPm);

        int daysRemaining = (totalDays - currentDay) + 1;
        daysLeftText.text = daysRemaining + (daysRemaining == 1 ? " Day Left" : " Days Left");
    }

    private void EndDay()
    {
        isTimerRunning = false;
        Time.timeScale = 0f;

        if (player != null && player.cache != null)
        {
            foodScoreText.text = "Food Collected: " + player.cache.food;
            mercuryScoreText.text = "Mercury Levels: " + player.cache.mercury;
        }

        dayOverUI.SetActive(true);
        currentDay++;
    }

    public void LoadNextDay()
    {
        if (currentDay <= totalDays)
        {
            StartDay();
        }
        else
        {
            Debug.Log("Game Finished!");
        }
    }
}