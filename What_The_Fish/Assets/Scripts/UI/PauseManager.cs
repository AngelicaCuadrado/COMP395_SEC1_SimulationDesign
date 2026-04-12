using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject comingSoonUI;
    [SerializeField] private GameObject confirmationUI;
    [SerializeField] private GameObject gameOverUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    // --- Pause / Resume ---

    public void Resume()
    {
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
        confirmationUI.SetActive(true); // UI_Pause stays visible behind the confirmation
    }

    public void CancelRestart()
    {
        confirmationUI.SetActive(false); // Just hide confirmation, UI_Pause is already visible
    }

    public void ConfirmRestart()
    {
        // Clears save — equivalent to New Beginning from Title Screen
        SaveManager.ClearSave();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- Game Over ---

    // Assigned to EndGame (test) button onClick — placeholder until game loop triggers this
    public void ShowGameOver()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    // --- Main Menu ---

    public void GoToMainMenu()
    {
        // Save progress — always fires so TitleScene shows RESUME GAME.
        // Cache values default to 0 until the full game loop is wired up.
        PlayerController player = FindFirstObjectByType<PlayerController>();
        int food    = player != null ? player.cache.food    : 0;
        int mercury = player != null ? player.cache.mercury : 0;
        SaveManager.SaveGame(food, mercury);
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
