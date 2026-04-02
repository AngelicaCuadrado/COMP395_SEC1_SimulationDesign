using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Hook hook;
    public Cache cache { get; private set; }

    private void Awake()
    {
        cache = new Cache();
        hook.SetPlayer(this);
    }
}
