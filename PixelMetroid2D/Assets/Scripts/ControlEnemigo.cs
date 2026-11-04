using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ControlEnemigo : MonoBehaviour
{
    [Header ("Movimiento")]
    public float velocidad = 2f;
    private Vector2 posicionInicial;
    public Vector2 posicionFinal;
    private bool movimientoHaciaArriba = true;
    [SerializeField] private float tolerancia = 0.1f;
    private SpriteRenderer renderizador; //
    private Animator animador;//


    private Rigidbody2D fisica;

    // componentes
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        renderizador = GetComponent<SpriteRenderer>();//
    }

    void Start()
    {
        posicionInicial = fisica.position;
    }

    //solo fisicas
    private void FixedUpdate()
    {

        if (velocidad <= 0)
        {
          return;
        }

        //if (movimientoHaciaArriba)
        //{
        //    posicionObjetivo = posicionFinal;
        //} else
        //{
        //    posicionObjetivo = posicionInicial;
        //}

        Vector2 posicionObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial;

        if (Vector2.Distance(fisica.position, posicionObjetivo)<= tolerancia)
        {

            //if (movimientoHaciaArriba)
            //{
            //    movimientoHaciaArriba = false;
            //    posicionObjetivo = posicionInicial;
            //} 
            //else
            //{
            //    movimientoHaciaArriba = true;
            //    posicionObjetivo = posicionFinal;
            //}

            movimientoHaciaArriba = !movimientoHaciaArriba;
            posicionObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial;

            Debug.Log("Posicion objetivo: " + posicionObjetivo);


        }

        OrientarSprite(posicionObjetivo);//

        // Muevete a esa posicion con un movimiento ()
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));

    }

    //
    private void OrientarSprite(Vector2 objetivo)
    {
        if (renderizador == null) return;

        if(objetivo.x > fisica.position.x)
        {
            renderizador.flipX = true;
        }

        else if (objetivo.x < fisica.position.x)
        {
            renderizador.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (!colision.gameObject.CompareTag("Player")) return;

        if (colision.gameObject.TryGetComponent(out MovimientoJugador jugador))
        {
            jugador.ReaparecerJugador();
        }

        Debug.LogError("El objeto Player necesita ControlJugador en la raíz.");

    }

    void Update()
    {

    }



}
