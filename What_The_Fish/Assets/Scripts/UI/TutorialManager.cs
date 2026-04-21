using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class TutorialStep
{
    [TextArea(3, 5)]
    public string sentence;
    public Sprite majorEmotion;
    public UnityEvent onStepEnter;
    public UnityEvent onStepExit;
}

[DefaultExecutionOrder(-100)]
public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image majorPortraitDisplay;

    [Header("Audio")]
    [SerializeField] private AudioClip nextDialogueSound;

    [Header("Panel Movement")]
    [SerializeField] private RectTransform panelRectTransform;
    public Vector2 positionTop = new Vector2(0, 350f);
    public Vector2 positionBottom = new Vector2(0, -350f);

    [Header("Tutorial Sequence")]
    [SerializeField] private TutorialStep[] steps;

    private int currentStepIndex = 0;
    private bool isTutorialActive = false;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartTutorial();
    }

    void Update()
    {
        if (isTutorialActive)
        {
            Time.timeScale = 0f;

            if (PauseManager.Instance != null && PauseManager.Instance.IsAnyMenuOpen())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                DisplayNextStep();
            }
        }
    }

    public void StartTutorial()
    {
        isTutorialActive = true;
        tutorialPanel.SetActive(true);
        currentStepIndex = 0;
        ShowStep(currentStepIndex);
    }

    public void DisplayNextStep()
    {
        if (AudioManager.Instance != null && nextDialogueSound != null)
        {
            AudioManager.Instance.PlaySound(nextDialogueSound);
        }

        steps[currentStepIndex].onStepExit?.Invoke();
        currentStepIndex++;

        if (currentStepIndex < steps.Length)
        {
            ShowStep(currentStepIndex);
        }
        else
        {
            EndTutorial();
        }
    }

    private void ShowStep(int index)
    {
        dialogueText.text = steps[index].sentence;

        if (steps[index].majorEmotion != null)
        {
            majorPortraitDisplay.sprite = steps[index].majorEmotion;
        }

        steps[index].onStepEnter?.Invoke();
    }

    private void EndTutorial()
    {
        isTutorialActive = false;
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MovePanelToTop() { if (panelRectTransform != null) panelRectTransform.anchoredPosition = positionTop; }
    public void MovePanelToBottom() { if (panelRectTransform != null) panelRectTransform.anchoredPosition = positionBottom; }
}