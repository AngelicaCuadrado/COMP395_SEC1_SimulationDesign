using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public void MoveTo(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
