using UnityEngine;

public class BookUIManager : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject bookCanvas;
    public Animator bookAnimator;

    public void OpenBook()
    {
        Time.timeScale = 0f;

        gameCanvas.SetActive(false);
        bookCanvas.SetActive(true);

        if (bookAnimator != null)
        {
            bookAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;

            bookAnimator.SetBool("Open", true);
            bookAnimator.SetBool("Close", false);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioOpenBook);
    }

    public void CloseBook()
    {
        Time.timeScale = 1f;

        if (bookAnimator != null)
        {
            bookAnimator.SetBool("Close", true);
            bookAnimator.SetBool("Open", false);
        }

        bookCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioCloseBook);
    }
}