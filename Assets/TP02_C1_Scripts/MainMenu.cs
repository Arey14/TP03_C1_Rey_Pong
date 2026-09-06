using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // 1. Referencias a Paneles del Menú
    [Header("Paneles del Menú")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    // 2. Referencias a Botones
    [Header("Botones del Menú Principal")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    [Header("Botones de Regreso (Opcional)")]
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button creditsBackButton;

    // 3. Control inicial y suscripción a eventos OnClick por código
    void Start()
    {
        if (playButton != null) playButton.onClick.AddListener(PlayGame);
        if (settingsButton != null) settingsButton.onClick.AddListener(OpenSettings);
        if (creditsButton != null) creditsButton.onClick.AddListener(OpenCredits);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);

        if (settingsBackButton != null) settingsBackButton.onClick.AddListener(ShowMainMenu);
        if (creditsBackButton != null) creditsBackButton.onClick.AddListener(ShowMainMenu);

        ShowMainMenu();
    }

    // 4. Lógica Botón Play
    public void PlayGame()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        Time.timeScale = 1f; 
    }

    // 5. Lógica Botón Settings
    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    // 6. Lógica Botón Credits
    public void OpenCredits()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    // 7. Lógica Botón Back
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    // 8. Lógica Botón Exit
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit();
        #endif
    }
}