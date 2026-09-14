using UnityEngine;

/// <summary>
/// Detecta cuando la pelota cruza el arco o sale de la cancha por los laterales,
/// reiniciando la pelota y opcionalmente sumando puntos.
/// </summary>
public class GoalTrigger : MonoBehaviour
{
    [Header("Configuración de Arco")]
    [Tooltip("Indica si este arco pertenece al Jugador 1 (true) o Jugador 2 (false)")]
    [SerializeField] private bool isPlayer1Goal = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        BallController ball = other.GetComponent<BallController>();
        if (ball != null)
        {
            // Reinicia la pelota al centro
            ball.ResetBall();
        }
    }
}
