using UnityEngine;

/// <summary>
/// Controla el movimiento físico y la personalización de la paleta del jugador.
/// Cumple con los requisitos de físicas (Rigidbody2D + AddForce), uso de fixedDeltaTime y configuración dinámica.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;

    [Header("Límites de la Cancha (Opcional si no usa colliders en bordes)")]
    [SerializeField] private bool usePositionClamping = true;
    [SerializeField] private float yBoundary = 4.2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float verticalInput = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Aseguramos configuración física adecuada para paletas 2D
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Lectura de inputs en Update
        verticalInput = 0f;
        if (Input.GetKey(upKey))
        {
            verticalInput += 1f;
        }
        if (Input.GetKey(downKey))
        {
            verticalInput -= 1f;
        }
    }

    void FixedUpdate()
    {
        // Movimiento físico mediante AddForce utilizando Time.fixedDeltaTime
        if (verticalInput != 0f)
        {
            Vector2 force = Vector2.up * (verticalInput * speed * Time.fixedDeltaTime * 50f);
            rb.AddForce(force, ForceMode2D.Force);
        }
        else
        {
            // Frenado suave cuando no hay input
            rb.linearVelocity = new Vector2(0f, Mathf.Lerp(rb.linearVelocity.y, 0f, Time.fixedDeltaTime * 10f));
        }

        // Restricción opcional de límites verticales
        if (usePositionClamping)
        {
            Vector3 clampedPos = transform.position;
            clampedPos.y = Mathf.Clamp(clampedPos.y, -yBoundary, yBoundary);
            transform.position = clampedPos;
        }
    }

    #region Métodos de Configuración y Personalización (Settings)

    /// <summary>
    /// Modifica la velocidad de desplazamiento de la paleta.
    /// </summary>
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Ajusta la altura de la paleta modificando la escala en Y.
    /// </summary>
    public void SetPaddleHeight(float newHeight)
    {
        Vector3 currentScale = transform.localScale;
        currentScale.y = newHeight;
        transform.localScale = currentScale;
    }

    public float GetPaddleHeight()
    {
        return transform.localScale.y;
    }

    /// <summary>
    /// Cambia el color del sprite de la paleta.
    /// </summary>
    public void SetPaddleColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }

    public Color GetPaddleColor()
    {
        return spriteRenderer != null ? spriteRenderer.color : Color.white;
    }

    #endregion
}
