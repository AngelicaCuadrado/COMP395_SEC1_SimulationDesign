using UnityEngine;

public class FishCuaghtUI : MonoBehaviour
{
    public static FishCuaghtUI instance;
    private Hook hook;

    [SerializeField]
    private GameObject CaughtUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCaughtUI(Hook hook, Fish fish) 
    {
        this.hook = hook;
        CaughtUI.SetActive(true);
    }

    public void OnKeepPressed()
    {
        hook.KeepFish();
        CaughtUI?.SetActive(false);
    }

    public void OnThrowPressed() 
    {
        hook.ThrowFish();
        CaughtUI?.SetActive(false);
    }
}
