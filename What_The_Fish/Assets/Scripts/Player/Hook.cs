using UnityEngine;

public class Hook : MonoBehaviour
{
    Cache cache;

    private void OnTriggerEnter(Collider other)
    {
        Fish fish;
        if (fish = other.GetComponent<Fish>()) 
        {
            Cache(fish);
        }   
    }

    private void Cache(Fish fish) 
    {

    }
}
