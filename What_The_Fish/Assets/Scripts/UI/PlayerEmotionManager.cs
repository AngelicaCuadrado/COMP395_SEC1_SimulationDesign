using UnityEngine;

public class PlayerEmotionManager : MonoBehaviour
{
    public enum EmotionState { Happy, Worried, Sad }
    public EmotionState CurrentState { get; private set; } = EmotionState.Sad;

    [Header("Emotion UI Objects")]
    [SerializeField] private GameObject happyImage;
    [SerializeField] private GameObject worriedImage;
    [SerializeField] private GameObject sadImage;

    [Header("References")]
    [SerializeField] private PlayerController player;

    [Header("Emotion Thresholds (ratio = food / (food + mercury))")]
    [SerializeField] private float happyRatio = 0.65f;  // food is 65%+ of total → happy
    [SerializeField] private float worriedRatio = 0.40f; // food is 40-65% → worried; below 40% → sad

    void Start()
    {
        // Player starts sad — nothing proven yet
        SetEmotion(false, false, true);
    }

    void Update()
    {
        if (player != null && player.cache != null)
        {
            UpdateEmotion(player.cache.mercury, player.cache.food);
        }
    }

    private void UpdateEmotion(int currentMercury, int currentFood)
    {
        int total = currentFood + currentMercury;

        if (total == 0)
        {
            SetEmotion(false, false, true); // no fish yet — stay sad
            return;
        }

        float ratio = (float)currentFood / total;

        if (ratio >= happyRatio)
        {
            CurrentState = EmotionState.Happy;
            SetEmotion(true, false, false);
        }
        else if (ratio >= worriedRatio)
        {
            CurrentState = EmotionState.Worried;
            SetEmotion(false, true, false);
        }
        else
        {
            CurrentState = EmotionState.Sad;
            SetEmotion(false, false, true);
        }
    }

    private void SetEmotion(bool showHappy, bool showWorried, bool showSad)
    {
        if (happyImage.activeSelf != showHappy)   happyImage.SetActive(showHappy);
        if (worriedImage.activeSelf != showWorried) worriedImage.SetActive(showWorried);
        if (sadImage != null && sadImage.activeSelf != showSad) sadImage.SetActive(showSad);
    }
}