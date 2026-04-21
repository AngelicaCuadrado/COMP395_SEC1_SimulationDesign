using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Cache
{
    public int food;
    public int mercury;

    public int maxFood    = 300;
    public int maxMercury = 300;

    public Image foodFillBar;
    public Image mercuryFillBar;

    public TextMeshProUGUI foodCountText;
    public TextMeshProUGUI mercuryCountText;
    public TextMeshProUGUI scoreText;

    [Header("Tracking")]
    public int fishCaught;
    public int correctSorts;

    [Header("Testing")]
    public float statMultiplier = 3f; // set to 1f for production

    [Header("Balancing")]
    public int mercuryReductionPerGoodFish = 5;
    public int earlyFinishBonus    =  200;
    public int mercuryMaxPenalty   = -200;

    private bool zoneEndTriggered = false;

    public void Keep(Fish fish)
    {
        bool isGood = fish.fishData != null && fish.fishData.isGoodFish;

        if (isGood)
        {
            food += Mathf.RoundToInt(fish.GetFood() * statMultiplier);
            correctSorts++;
        }
        else
        {
            mercury += Mathf.RoundToInt(fish.GetMercury() * statMultiplier);
        }

        fishCaught++;
        food    = Mathf.Min(food,    maxFood);
        mercury = Mathf.Min(mercury, maxMercury);

        RefreshUI();
        CheckZoneEndConditions();
    }

    public void Throw(Fish fish)
    {
        fishCaught++; // counts all fish encountered, regardless of decision

        if (fish != null && fish.fishData != null && !fish.fishData.isGoodFish)
            correctSorts++;

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (foodFillBar    != null) foodFillBar.fillAmount    = (float)food    / maxFood;
        if (mercuryFillBar != null) mercuryFillBar.fillAmount = (float)mercury / maxMercury;
        if (foodCountText    != null) foodCountText.text    = food    + "/" + maxFood;
        if (mercuryCountText != null) mercuryCountText.text = mercury + "/" + maxMercury;

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText == null) return;
        int current  = food * 10 - mercury * 15 + correctSorts * 5;
        int previous = SaveManager.GetCumulativeScore();
        scoreText.text = Mathf.Max(0, current + previous).ToString();
    }

    private void CheckZoneEndConditions()
    {
        if (zoneEndTriggered) return;

        if (food >= maxFood)
        {
            zoneEndTriggered = true;
            TimeManager.instance?.EndZoneEarly(true);
        }
        else if (mercury >= maxMercury)
        {
            zoneEndTriggered = true;
            TimeManager.instance?.EndZoneEarly(false);
        }
    }

    public void ResetFoodForNewDay()
    {
        food = 0;
        if (foodFillBar  != null) foodFillBar.fillAmount = 0f;
        if (foodCountText != null) foodCountText.text    = "0/" + maxFood;
    }

    public void ResetForNewZone(int newMaxFood, int newMaxMercury)
    {
        food    = 0;
        mercury = 0;
        maxFood      = newMaxFood;
        maxMercury   = newMaxMercury;
        zoneEndTriggered = false;

        if (foodFillBar    != null) foodFillBar.fillAmount    = 0f;
        if (mercuryFillBar != null) mercuryFillBar.fillAmount = 0f;
        if (foodCountText    != null) foodCountText.text    = "0/" + maxFood;
        if (mercuryCountText != null) mercuryCountText.text = "0/" + maxMercury;

        UpdateScore();
    }
}
