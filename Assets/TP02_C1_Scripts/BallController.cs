using System.Collections;
using UnityEngine;

/// <summary>
/// Controla el movimiento físico de la pelota, su lanzamiento, rebote y aceleración tras cada impacto con las paletas.
/// No se lanza si el menú principal está activo o si el juego está pausado.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [Header("Configuración de Velocidad")]
    [SerializeField] private float initialSpeed = 8f;
    [SerializeField] private float speedIncreasePerHit = 1.1f; // Multiplicador de velocidad por impacto
    [SerializeField] private float maxSpeed = 25f;

    [Header("Configuración de Lanzamiento")]
    [SerializeField] private float startDelay = 1.5f; // Tiempo de espera antes de comenzar a moverse
    [SerializeField] private bool autoLaunchOnStart = false; // Solo se lanza si no hay MainMenu o si se solicita explícitamente

    private Rigidbody2D rb;
    private float currentSpeed;
    private Vector2 startPosition;
    private Coroutine launchCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Start()
    {
        // Detener la pelota en el origen al inicio
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;

        // Si se especificó auto-lanzamiento (por ejemplo si no se usa MainMenu), lanzar tras delay
        if (autoLaunchOnStart)
        {
            ResetBall();
        }
    }

    /// <summary>
    /// Reinicia la posición y lanza la pelota tras una pausa de espera.
    /// </summary>
    public void ResetBall()
    {
        if (launchCoroutine != null)
        {
            StopCoroutine(launchCoroutine);
        }
        launchCoroutine = StartCoroutine(ResetAndLaunchRoutine());
    }

    private IEnumerator ResetAndLaunchRoutine()
    {
        // Detener la pelota en el origen
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        currentSpeed = initialSpeed;

        // Esperar el tiempo configurado (respeta la pausa si Time.timeScale == 0)
        yield return new WaitForSeconds(startDelay);

        LaunchBall();
    }

    /// <summary>
    /// Lanza la pelota hacia uno de los lados con un ángulo vertical aleatorio usando AddForce.
    /// </summary>
    private void LaunchBall()
    {
        // Elegir dirección horizontal aleatoria (izquierda o derecha)
        float dirX = Random.value < 0.5f ? -1f : 1f;

        // Elegir ángulo vertical moderado entre -0.5 y 0.5
        float dirY = Random.Range(-0.5f, 0.5f);

        Vector2 launchDirection = new Vector2(dirX, dirY).normalized;

        // Aplicamos fuerza de impulso físico (ForceMode2D.Impulse)
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(launchDirection * currentSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el impacto fue contra una paleta de jugador
        PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
        if (paddle != null)
        {
            HandlePaddleBounce(collision, paddle);
        }
    }

    /// <summary>
    /// Calcula la dirección de rebote angular según el punto de contacto en la paleta
    /// y acelera la velocidad de la pelota por impacto.
    /// </summary>
    private void HandlePaddleBounce(Collision2D collision, PaddleController paddle)
    {
        // Incrementar la velocidad de la pelota por cada impacto
        currentSpeed = Mathf.Min(currentSpeed * speedIncreasePerHit, maxSpeed);

        // Determinar si la paleta está a la izquierda o derecha de la pelota
        float ballX = transform.position.x;
        float paddleX = paddle.transform.position.x;
        float dirX = (ballX > paddleX) ? 1f : -1f;

        // Calcular el punto de impacto relativo en Y respecto al centro de la paleta
        float paddleHeight = paddle.GetPaddleHeight();
        float hitOffset = transform.position.y - paddle.transform.position.y;
        float normalizedHit = hitOffset / (paddleHeight * 0.5f);
        normalizedHit = Mathf.Clamp(normalizedHit, -1f, 1f);

        // Vector de salida con ángulo calculado
        Vector2 bounceDirection = new Vector2(dirX, normalizedHit).normalized;

        // Aplicamos la nueva velocidad física manteniendo la consistencia con Rigidbody2D
        rb.linearVelocity = bounceDirection * currentSpeed;
    }

    /// <summary>
    /// Obtiene la velocidad actual de la pelota.
    /// </summary>
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
