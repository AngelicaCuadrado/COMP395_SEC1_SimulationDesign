using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Cache
{
    public int food;
    public int mercury;

    public int maxFood = 100;
    public int maxMercury = 100;

    public Image foodFillBar;
    public Image mercuryFillBar;

    public void Keep(Fish fish)
    {
        food += fish.GetFood();
        mercury += fish.GetMercury();

        if (foodFillBar != null)
        {
            foodFillBar.fillAmount = (float)food / maxFood;
        }

        if (mercuryFillBar != null)
        {
            mercuryFillBar.fillAmount = (float)mercury / maxMercury;
        }
    }

    public void Throw()
    {
        return;
    }
}