using UnityEngine;

public class TutorialBookController : MonoBehaviour
{
    [Header("Book Reference")]
    public Animator bookAnimator;

    public void OpenBookForTutorial()
    {
        gameObject.SetActive(true);

        if (bookAnimator != null)
        {
            bookAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;

            bookAnimator.SetBool("Open", true);
            bookAnimator.SetBool("Close", false);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioOpenBook);
    }

    public void FlipToBadFish()
    {
        if (bookAnimator != null)
        {
            bookAnimator.SetTrigger("Next");
        }
    }

    public void CloseBookForTutorial()
    {
        if (bookAnimator != null)
        {
            bookAnimator.SetBool("Close", true);
            bookAnimator.SetBool("Open", false);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound(AudioManager.Instance.audioCloseBook);
    }
}