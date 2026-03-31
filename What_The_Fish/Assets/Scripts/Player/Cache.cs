using UnityEngine;

public class Cache
{
    public int food;
    public int mercury;

    public void Keep(Fish fish) 
    {
        food += fish.GetFood();
        mercury += fish.GetMercury();
    }

    public void Throw()
    {
        return;
    }
}
