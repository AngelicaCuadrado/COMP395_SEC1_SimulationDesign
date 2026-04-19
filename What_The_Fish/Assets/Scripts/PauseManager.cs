using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject comingSoonUI;
    [SerializeField] private GameObject confirmationUI;
    [SerializeField] private GameObject gameOverUI;

    [Header("Special UIs (To Prevent Resume)")]
    [SerializeField] private GameObject bookUI;
    [SerializeField] private GameObject caughtUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsSpecialUIActive()) return;

            if (isPaused) Resume();
            else Pause();
        }
    }

    // --- Pause / Resume ---

    public void Resume()
    {
        if (IsSpecialUIActive()) return;

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // --- Glossary (Coming Soon) ---

    public void OpenGlossary()
    {
        pauseUI.SetActive(false);
        comingSoonUI.SetActive(true);
    }

    public void CloseComingSoon()
    {
        comingSoonUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    // --- Restart Confirmation ---

    public void ShowRestartConfirmation()
    {
        confirmationUI.SetActive(true);
    }

    public void CancelRestart()
    {
        confirmationUI.SetActive(false);
    }

    public void ConfirmRestart()
    {
        SaveManager.ClearSave();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- Game Over ---

    // isWin: true = happy/worried ending, false = sad ending
    public void ShowGameOver(bool isWin = true)
    {
        pauseUI.SetActive(false);
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Debug.Log(isWin ? "LEVEL WIN" : "LEVEL LOSE");
    }

    // --- Main Menu ---

    public void GoToMainMenu()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        int food = player != null ? player.cache.food : 0;
        int mercury = player != null ? player.cache.mercury : 0;

        SaveManager.SaveGame(food, mercury);
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    // --- Helper Logic ---

    private bool IsSpecialUIActive()
    {
        bool isBookOpen = bookUI != null && bookUI.activeInHierarchy;
        bool isCaughtOpen = caughtUI != null && caughtUI.activeInHierarchy;

        return isBookOpen || isCaughtOpen;
    }
}