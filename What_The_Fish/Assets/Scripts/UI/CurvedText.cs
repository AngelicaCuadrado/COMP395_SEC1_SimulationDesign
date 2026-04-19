using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class CurvedText : MonoBehaviour
{
    [Tooltip("Higher = flatter curve. Lower = tighter curve.")]
    public float radius = 200f;

    private TMP_Text tmpText;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        if (tmpText == null) return;

        tmpText.ForceMeshUpdate();
        var textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                Vector3 orig = verts[charInfo.vertexIndex + j];
                float angle = orig.x / radius * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, -angle);
                verts[charInfo.vertexIndex + j] = rotation * orig + new Vector3(0, radius, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
