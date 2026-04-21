using UnityEngine;
using UnityEngine.UI;
public class UIButtonShaderUnscaledTime : MonoBehaviour
{
    [SerializeField] private string timeProperty = "_ManualTime";
    private Image img;
    private Material runtimeMat;

    void Awake()
    {
        img = GetComponent<Image>();

        if (img != null && img.material != null)
        {
            runtimeMat = new Material(img.material);
            img.material = runtimeMat;
        }
    }

    void Update()
    {
        if (runtimeMat != null)
        {
            runtimeMat.SetFloat(timeProperty, Time.unscaledTime);
        }
    }
}
