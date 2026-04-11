using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "Level_1";

    [Header("Start Button")]
    [SerializeField] private TextMeshProUGUI startButtonText;

    [Header("Prompts")]
    [SerializeField] private GameObject resumeGamePrompt;  // RESUMEGAME panel
    [SerializeField] private GameObject saveQuitPrompt;    // SAVEQUIT panel

    [Header("Settings")]
    [SerializeField] private GameObject volumeControl;  // Volume Control GameObject
    [SerializeField] private GameObject startButton;    // Start btn — hidden when settings open
    [SerializeField] private GameObject settingsButton; // Settings btn — hidden when settings open
    [SerializeField] private Slider volumeSlider;       // Slider inside Volume Control

    void Start()
    {
        // Swap button text based on whether a save exists
        if (startButtonText != null)
            startButtonText.text = SaveManager.HasSave() ? "RESUME GAME" : "START GAME";

        // Always start with prompts and settings hidden
        if (resumeGamePrompt != null) resumeGamePrompt.SetActive(false);
        if (saveQuitPrompt   != null) saveQuitPrompt.SetActive(false);
        if (volumeControl    != null) volumeControl.SetActive(false);

        // Initialise slider to match current volume
        if (volumeSlider != null)
            volumeSlider.value = AudioListener.volume;
    }

    // --- Start / Resume ---

    // Assigned to Start btn onClick
    public void OnStartButtonPressed()
    {
        if (SaveManager.HasSave())
        {
            // Show prompt: Continue Last Save or New Beginning
            resumeGamePrompt.SetActive(true);
        }
        else
        {
            LoadFreshGame();
        }
    }

    // Assigned to "Continue Saved Game" button inside RESUMEGAME prompt
    public void ContinueSavedGame()
    {
        // Cache will pick up saved values via SaveManager.GetFood/GetMercury
        // when PlayerController initialises in Level_1
        SceneManager.LoadScene(gameSceneName);
    }

    // Assigned to "New Beginning" button inside RESUMEGAME prompt
    public void NewBeginning()
    {
        SaveManager.ClearSave();
        LoadFreshGame();
    }

    private void LoadFreshGame()
    {
        if (resumeGamePrompt != null) resumeGamePrompt.SetActive(false);
        SceneManager.LoadScene(gameSceneName);
    }

    // --- Quit ---

    // Assigned to Quit btn (red X) onClick
    public void OnQuitPressed()
    {
        saveQuitPrompt.SetActive(true);
    }

    // Assigned to "Save & Quit" button inside SAVEQUIT prompt
    // Note: progress was already saved by PauseManager when returning to this screen.
    // This just quits the application.
    public void SaveAndQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Assigned to "Quit Without Saving" button inside SAVEQUIT prompt
    public void QuitWithoutSaving()
    {
        SaveManager.ClearSave();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Assigned to any back/cancel button inside prompts
    public void ClosePrompts()
    {
        if (resumeGamePrompt != null) resumeGamePrompt.SetActive(false);
        if (saveQuitPrompt   != null) saveQuitPrompt.SetActive(false);
    }

    // --- Settings ---

    // Assigned to Settings btn onClick
    public void OpenSettings()
    {
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        volumeControl.SetActive(true);
    }

    // Assigned to Cancel btn inside Volume Control onClick
    public void CloseSettings()
    {
        volumeControl.SetActive(false);
        startButton.SetActive(true);
        settingsButton.SetActive(true);
    }

    // Assigned to Slider OnValueChanged — fires automatically as slider moves
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
