using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    // 1. Referencias Jugador 1
    [Header("Configuración Jugador 1")]
    [SerializeField] private Movement player1;
    [SerializeField] private Slider speedSliderP1;
    [SerializeField] private TextMeshProUGUI speedTextP1;

    // 2. Referencias Jugador 2
    [Header("Configuración Jugador 2")]
    [SerializeField] private Movement player2;
    [SerializeField] private Slider speedSliderP2;
    [SerializeField] private TextMeshProUGUI speedTextP2;

    // 3. Inicialización de valores
    void Start()
    {
        InitializeSliders();
    }

    void OnEnable()
    {
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        // Configurar Jugador 1
        if (player1 != null && speedSliderP1 != null)
        {
            speedSliderP1.value = player1.GetSpeed();
            UpdatePlayer1Speed(speedSliderP1.value);
            speedSliderP1.onValueChanged.RemoveListener(UpdatePlayer1Speed);
            speedSliderP1.onValueChanged.AddListener(UpdatePlayer1Speed);
        }

        // Configurar Jugador 2
        if (player2 != null && speedSliderP2 != null)
        {
            speedSliderP2.value = player2.GetSpeed();
            UpdatePlayer2Speed(speedSliderP2.value);
            speedSliderP2.onValueChanged.RemoveListener(UpdatePlayer2Speed);
            speedSliderP2.onValueChanged.AddListener(UpdatePlayer2Speed);
        }
    }

    // 4. Lógica Slider Jugador 1
    public void UpdatePlayer1Speed(float value)
    {
        if (player1 != null)
        {
            player1.SetSpeed(value);
        }

        if (speedTextP1 != null)
        {
            speedTextP1.text = value.ToString("0.0");
        }
    }

    // 5. Lógica Slider Jugador 2
    public void UpdatePlayer2Speed(float value)
    {
        if (player2 != null)
        {
            player2.SetSpeed(value);
        }

        if (speedTextP2 != null)
        {
            speedTextP2.text = value.ToString("0.0");
        }
    }
}
