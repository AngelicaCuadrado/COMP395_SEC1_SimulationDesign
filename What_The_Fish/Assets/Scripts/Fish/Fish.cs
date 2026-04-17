using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishData fishData;

    [SerializeField]
    int mercury;
    [SerializeField]
    int food;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetFood() {  return food; }
    public int GetMercury() {  return mercury; }
}
