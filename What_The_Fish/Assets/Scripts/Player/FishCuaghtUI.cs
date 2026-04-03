using UnityEngine;

public class FishCuaghtUI : MonoBehaviour
{
    public static FishCuaghtUI instance;
    [SerializeField]
    private GameObject CaughtUI;
    private Hook hook;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
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
