using UnityEngine;

public class FishCrontroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private Transform target;

    void Update()
    {
        if(target == null) { return; }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
    }
}
