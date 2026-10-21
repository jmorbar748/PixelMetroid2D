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

    private Rigidbody2D fisica;

    // componentes
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
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

        // Muevete a esa posicion con un movimiento ()
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));

    }

    void Update()
    {

    }



}
