using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI survivorsText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Stars")]
    [SerializeField] private Image[] starImages;
    private static readonly Color StarOn = new Color(1f, 0.84f, 0f);
    private static readonly Color StarOff = new Color(0.4f, 0.4f, 0.4f);

    public void ShowResults(int survivors, int stars)
    {
        // Survivors
        if (survivorsText != null)
            survivorsText.text = $"Survivors: {survivors}/300";

        // Message
        if (messageText != null)
        {
            if (survivors >= 250)
                messageText.text = "Amazing work! The village thrives.";
            else if (survivors >= 150)
                messageText.text = "Many were lost, but we survive.";
            else
                messageText.text = "The village suffered greatly...";
        }

        // Stars
        ApplyStars(stars);
    }

    private void ApplyStars(int stars)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (starImages[i] != null)
                starImages[i].color = i < stars ? StarOn : StarOff;
        }
    }
}
