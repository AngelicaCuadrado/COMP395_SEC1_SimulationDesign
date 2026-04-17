using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class TutorialStep
{
    [TextArea(3, 5)]
    public string sentence;
    public UnityEvent onStepEnter;
    public UnityEvent onStepExit;
}

[DefaultExecutionOrder(-100)]
public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Tutorial Sequence")]
    [SerializeField] private TutorialStep[] steps;

    private int currentStepIndex = 0;
    private bool isTutorialActive = false;

    void Awake()
    {
        Time.timeScale = 0f;
        Debug.Log("Tutorial: Intentando pausar en Awake");
    }

    void Start()
    {
        StartTutorial();
    }

    void Update()
    {
        if (isTutorialActive)
        {
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
                Debug.LogWarning("¡ALERTA! Otro script intentó reanudar el tiempo, pero el Major Bobber lo bloqueó.");
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
        steps[index].onStepEnter?.Invoke();
        Time.timeScale = 0f;
    }

    private void EndTutorial()
    {
        isTutorialActive = false;
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Tutorial terminado: Tiempo reanudado.");
    }
}