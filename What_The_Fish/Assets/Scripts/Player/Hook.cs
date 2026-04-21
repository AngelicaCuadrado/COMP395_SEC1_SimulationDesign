using UnityEngine;

public class Hook : MonoBehaviour
{
    private PlayerController player;
    private Fish caughtFish;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hook detects trigger");
        Fish fish;
        if (fish = other.GetComponent<Fish>()) 
        {
            Debug.Log("Found Fish");
            caughtFish = fish;
            FishCuaghtUI.instance.ShowCaughtUI(this, caughtFish);
        }   
    }


    public void SetPlayer(PlayerController player) 
    {
        this.player = player;
    }

    public void KeepFish() 
    {
        player.cache.Keep(caughtFish);
        Destroy(caughtFish.gameObject);
        caughtFish = null;
    }

    public void ThrowFish()
    {
        player.cache.Throw(caughtFish);
        Destroy(caughtFish.gameObject);
        caughtFish = null;
    }
}
