using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocity;

    // Public InputActionReference accionMover;

    // Permite modificarlo desde aqui y desde el inspector
    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    [SerializeField] private InputActionReference accionSaltar;
    public float fuerzaImpulso;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D fisica;
    private Vector2 entradaX;
    private bool saltoPendiente;
    private bool estaTocandoSuelo;
    private float longitudRayCast = 1.6f;

    // Start se ejecuta una sola vez (cuando el gameobject este activo en la escena y jugador tambien)
    // Awake se ejecuta aunque el script no este activo
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        // <- (-1,0) / (1,0) ->
        entradaX = accionMover.action.ReadValue<Vector2>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRayCast, capaSuelo);

        estaTocandoSuelo = hit.collider != null;

        Color colorRayo = estaTocandoSuelo ? Color.green : Color.red;

        Debug.DrawRay(transform.position, Vector2.down * longitudRayCast, colorRayo);

        // Si se pulsa el botón, el salto pendiente se queda en true
        if(accionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo)
        {
            saltoPendiente = true;
        }

    }

    private void OnEnable()
    {
        if ((accionMover == null) || accionMover.action == null)
        {
            Debug.LogError("Asigna mapa de acción Jugador/Mover en acciónMover");
            enabled = false;
            return;
        }
        accionMover.action.Enable();
        accionSaltar.action.Enable();

        //3
        if ((accionSaltar == null) || accionSaltar.action == null)
        {
            Debug.LogError("Asigna mapa de acción Jugador/Saltar en acciónSaltar");
            enabled = false;
            return;
        }
        accionMover.action.Enable();
        accionSaltar.action.Enable();

    }
    


    //3
        private void OnDisable()
    {
        if (accionMover != null && accionMover.action != null)
        {
            accionMover.action.Disable();
        }

        if (accionSaltar != null && accionSaltar.action != null)
        {
            accionSaltar.action.Disable();
        }

    }

    private void FixedUpdate()
    {
        // Se multiplica ((nuestra entrada izquierda -1 derecha +1) * (velocity = velocidad que hayamos impuesto))
        fisica.linearVelocity = new Vector2(entradaX.x * velocity, fisica.linearVelocity.y);

        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        }

        saltoPendiente = false;
    }

}
