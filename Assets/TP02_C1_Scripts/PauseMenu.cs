using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // 1. Referencias a Paneles de UI
    [Header("Paneles de UI")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject mainMenuPanel;

    // 2. Referencias a Botones de Pausa
    [Header("Botones de Pausa")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    [Header("Botones de Regreso a Pausa (Opcional)")]
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button creditsBackButton;

    private Canvas pauseCanvas;
    private bool isPaused = false;

    void Awake()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    // 3. Inicialización y suscripción a eventos OnClick por código
    void Start()
    {
        if (continueButton != null) continueButton.onClick.AddListener(Resume);
        if (settingsButton != null) settingsButton.onClick.AddListener(OpenSettings);
        if (creditsButton != null) creditsButton.onClick.AddListener(OpenCredits);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);

        if (settingsBackButton != null) settingsBackButton.onClick.AddListener(BackToPauseMenu);
        if (creditsBackButton != null) creditsBackButton.onClick.AddListener(BackToPauseMenu);

        Resume();
    }

    // 4. Detección de tecla Escape
    void Update()
    {
        // Si el menú principal está en pantalla, no permitimos pausar
        if (mainMenuPanel != null && mainMenuPanel.activeInHierarchy)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // 5. Lógica Botón Continue
    public void Resume()
    {
        SetPauseUIVisible(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        Time.timeScale = 1f; 
        isPaused = false;
    }

    // 6. Lógica de Pausa
    public void Pause()
    {
        SetPauseUIVisible(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        Time.timeScale = 0f;  
        isPaused = true;
    }

    // Activa o desactiva la visibilidad del menú de pausa sin apagar el script
    private void SetPauseUIVisible(bool visible)
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = visible;
        }
        else if (pauseMenuPanel != null && pauseMenuPanel != this.gameObject)
        {
            pauseMenuPanel.SetActive(visible);
        }
        else if (pauseMenuPanel != null && pauseMenuPanel == this.gameObject)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(visible);
            }
        }
    }

    // 7. Lógica Botón Settings
    public void OpenSettings()
    {
        SetPauseUIVisible(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    // 8. Lógica Botón Credits 
    public void OpenCredits()
    {
        SetPauseUIVisible(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    // 9. Lógica Botón Back
    public void BackToPauseMenu()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        SetPauseUIVisible(true);
    }

    // 10. Lógica Botón Exit
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit(); 
        #endif
    }
}
