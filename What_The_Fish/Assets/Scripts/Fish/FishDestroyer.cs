using UnityEngine;

public class FishDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Fish>()  != null) 
        {
            Destroy(other.gameObject);
        }
    }
}
