using UnityEngine;

public class BookUIManager : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject bookCanvas;
    public Animator bookAnimator;

    public void OpenBook()
    {
        gameCanvas.SetActive(false);
        bookCanvas.SetActive(true);

        if (bookAnimator != null)
        {
            bookAnimator.SetBool("Open", true);
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.audioOpenBook);
    }

    public void CloseBook()
    {
        bookCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        if (bookAnimator != null)
        {
            bookAnimator.SetBool("Close", true);
            bookAnimator.SetBool("Open", false);
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.audioCloseBook);
    }
}