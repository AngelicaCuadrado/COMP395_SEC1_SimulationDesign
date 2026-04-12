using UnityEngine;

public class PlayerEmotionManager : MonoBehaviour
{
    [Header("Emotion UI Objects")]
    [SerializeField] private GameObject happyImage;
    [SerializeField] private GameObject worriedImage;
    [SerializeField] private GameObject sadImage;

    [Header("References")]
    [SerializeField] private PlayerController player;

    [Header("Emotion Thresholds")]
    [SerializeField] private int warningMercuryLevel = 20;
    [SerializeField] private int dangerMercuryLevel = 50;

    void Update()
    {
        if (player != null && player.cache != null)
        {
            UpdateEmotion(player.cache.mercury, player.cache.food);
        }
    }

    private void UpdateEmotion(int currentMercury, int currentFood)
    {
        if (currentMercury >= dangerMercuryLevel)
        {
            SetEmotion(false, false, true);
        }
        else if (currentMercury > currentFood || currentMercury >= warningMercuryLevel)
        {
            SetEmotion(false, true, false);
        }
        else
        {
            SetEmotion(true, false, false);
        }
    }

    private void SetEmotion(bool showHappy, bool showWorried, bool showSad)
    {
        if (happyImage.activeSelf != showHappy) happyImage.SetActive(showHappy);
        if (worriedImage.activeSelf != showWorried) worriedImage.SetActive(showWorried);
        if (sadImage.activeSelf != showSad) sadImage.SetActive(showSad);
    }
}