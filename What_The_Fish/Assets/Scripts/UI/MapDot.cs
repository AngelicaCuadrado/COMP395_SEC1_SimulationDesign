using UnityEngine;
using UnityEngine.UI;

public class MapDot : MonoBehaviour
{
    private Image dotImage;

    [Header("Blinck Parameters")]
    public float blinkSpeed = 2f;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1.0f;

    void Awake()
    {
        dotImage = GetComponent<Image>();
    }

    void Update()
    {
        if (dotImage != null)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.unscaledTime * blinkSpeed, 1f));
            Color newColor = dotImage.color;
            newColor.a = alpha;
            dotImage.color = newColor;
        }
    }
}