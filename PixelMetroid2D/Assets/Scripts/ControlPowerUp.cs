using UnityEngine;

public class ControlPowerUp : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int cantidad = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.gameObject.TryGetComponent(out MovimientoJugador jugador))
        {
            //LLamar al método público que incrementa
            jugador.IncrementarPuntos(cantidad);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("El objeto Player necesita un componente MovimientoJugador.");
        }
    }

}
