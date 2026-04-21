using UnityEngine;

public class FishingLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Header("Referencias")]
    public Transform rodTip;
    public Transform hook;

    [Header("Configuración de Curva")]
    [Range(2, 30)]
    public int precision = 15;
    public float sagAmount = 1.5f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        if (rodTip != null && hook != null)
        {
            DrawCurvedLine();
        }
    }

    void DrawCurvedLine()
    {
        lineRenderer.positionCount = precision;
        Vector3 start = rodTip.position;
        Vector3 end = hook.position;
        Vector3 controlPoint = (start + end) / 2f;
        controlPoint.y -= sagAmount;

        for (int i = 0; i < precision; i++)
        {
            float t = i / (float)(precision - 1);
            Vector3 point = Vector3.Lerp(Vector3.Lerp(start, controlPoint, t),
                                         Vector3.Lerp(controlPoint, end, t), t);

            lineRenderer.SetPosition(i, point);
        }
    }
}