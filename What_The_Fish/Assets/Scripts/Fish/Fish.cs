using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    int mercury;
    [SerializeField]
    int food;
    public bool isGoodFish;

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
