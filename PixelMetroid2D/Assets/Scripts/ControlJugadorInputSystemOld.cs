using UnityEngine;

public class ControlJugadorInputSystemOld : MonoBehaviour
{
    public float velocidad;
    private float entradaX;

    //No se pone en los componentes porque ya lo está
    private Rigidbody2D fisica;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtengo el componente RigidBody del player para modificar posteriormente su linearVelocity
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //  Vector2 position = transform.position;

    //position.x = position.x + 0.1f;

    //transform.position = position;
    //}

    private void FixedUpdate()
    {
        //determina si va a la izquierda (-1 o negativo) o derecha (+1 o positivo)
        entradaX = Input.GetAxis("Horizontal");

        if (fisica != null)
        {
            fisica.linearVelocity = new Vector2(entradaX * velocidad, fisica.linearVelocity.y);
        }
        else
            Debug.LogError("El GameObject no tiene asignado un componente RigidBody2D que es necesario para moverlo");
    }

}
