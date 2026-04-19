using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private TextMeshProUGUI totalFoodText;
    [SerializeField] private TextMeshProUGUI totalMercuryText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI caughtCounterText;
    [SerializeField] private TextMeshProUGUI correctSortingText;
    [SerializeField] private TextMeshProUGUI mayorsMessageText;

    [Header("Stars Level 1")]
    [SerializeField] private Image[] level1Stars;
    [Header("Stars Level 2")]
    [SerializeField] private Image[] level2Stars;
    [Header("Stars Level 3")]
    [SerializeField] private Image[] level3Stars;

    [Header("Mayor Mood Portraits")]
    [SerializeField] private GameObject mayorHappy;
    [SerializeField] private GameObject mayorWorried;
    [SerializeField] private GameObject mayorSad;

    private static readonly Color StarEarned   = new Color(1f, 0.84f, 0f);
    private static readonly Color StarUnearned = new Color(0.4f, 0.4f, 0.4f);

    void OnEnable()
    {
        PopulateStats();
    }

    private void PopulateStats()
    {
        PlayerController player      = FindFirstObjectByType<PlayerController>();
        PlayerEmotionManager emotion = FindFirstObjectByType<PlayerEmotionManager>();

        // Grand totals across all 3 levels
        int totalFood = SaveManager.GetLevelFoodCollected(1)
                      + SaveManager.GetLevelFoodCollected(2)
                      + SaveManager.GetLevelFoodCollected(3);

        int totalFoodNeeded = SaveManager.GetLevelFoodNeeded(1)
                            + SaveManager.GetLevelFoodNeeded(2)
                            + SaveManager.GetLevelFoodNeeded(3);

        int totalMerc = SaveManager.GetLevelMercCollected(1)
                      + SaveManager.GetLevelMercCollected(2)
                      + SaveManager.GetLevelMercCollected(3);

        int totalMercNeeded = SaveManager.GetLevelMercNeeded(1)
                            + SaveManager.GetLevelMercNeeded(2)
                            + SaveManager.GetLevelMercNeeded(3);

        int totalScore = SaveManager.GetCumulativeScore();

        if (totalFoodText    != null) totalFoodText.text    = totalFood + "/" + totalFoodNeeded;
        if (totalMercuryText != null) totalMercuryText.text = totalMerc + "/" + totalMercNeeded;
        if (totalScoreText   != null) totalScoreText.text   = totalScore.ToString();

        if (player != null)
        {
            if (caughtCounterText  != null) caughtCounterText.text  = player.cache.fishCaught.ToString();
            if (correctSortingText != null) correctSortingText.text = player.cache.correctSorts.ToString();
        }

        ApplyStars(level1Stars, SaveManager.GetLevelStars(1));
        ApplyStars(level2Stars, SaveManager.GetLevelStars(2));
        ApplyStars(level3Stars, SaveManager.GetLevelStars(3));

        PlayerEmotionManager.EmotionState state = emotion != null
            ? emotion.CurrentState
            : PlayerEmotionManager.EmotionState.Sad;

        SetMayorMood(state);
        SetMayorMessage(state);
    }

    private void ApplyStars(Image[] stars, int earned)
    {
        if (stars == null) return;
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] != null)
                stars[i].color = i < earned ? StarEarned : StarUnearned;
        }
    }

    private void SetMayorMood(PlayerEmotionManager.EmotionState state)
    {
        if (mayorHappy   != null) mayorHappy.SetActive(state   == PlayerEmotionManager.EmotionState.Happy);
        if (mayorWorried != null) mayorWorried.SetActive(state == PlayerEmotionManager.EmotionState.Worried);
        if (mayorSad     != null) mayorSad.SetActive(state     == PlayerEmotionManager.EmotionState.Sad);
    }

    private void SetMayorMessage(PlayerEmotionManager.EmotionState state)
    {
        if (mayorsMessageText == null) return;

        if (state == PlayerEmotionManager.EmotionState.Happy)
            mayorsMessageText.text = "Wonderful! The town is well fed. You are a hero!";
        else if (state == PlayerEmotionManager.EmotionState.Worried)
            mayorsMessageText.text = "Not bad... but the town needed more. Keep trying.";
        else
            mayorsMessageText.text = "The town is struggling. Mercury levels are too high.";
    }
}
