using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleInput : MonoBehaviour
{
    //pra diferenciar o paddle esquerdo do direito
    public bool isLeftPaddle = true;
    public float speed = 6f;

    //referência para o input actions que eu fiz pros controles do jogo especificamente    
    private PongControls controls;
    private Rigidbody2D rb;
    private float moveY;

    void Awake()
    {
        controls = new PongControls();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        controls.Enable();
        
        //controles se for o script como componente do paddle esquerdo
        if (isLeftPaddle)
        {
            controls.Player.MoveLeft.performed += ctx => moveY = ctx.ReadValue<float>();
            controls.Player.MoveLeft.canceled += ctx => moveY = 0;
        }
        //controles se for o script como componente do paddle direito
        else
        {
            controls.Player.MoveRight.performed += ctx => moveY = ctx.ReadValue<float>();
            controls.Player.MoveRight.canceled += ctx => moveY = 0;
        }
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        //aplica a velocidade no rigidbody do paddle, somente no eixo y
        rb.linearVelocity = new Vector2(0, moveY * speed);
        
        //pega a velocidade atual do rigidbody
        Vector2 velocity = rb.linearVelocity;

        // "freia" o paddle quando ele chega no limite superior
        if (rb.position.y >= 4.4f && velocity.y > 0)
        {
            velocity.y = 0;
        }

        // "freia" o paddle quando ele chega no limite inferior
        if (rb.position.y <= -4.4f && velocity.y < 0)
        {    
            velocity.y = 0;
        }
        
        //devolve a velocidade corrigida
        rb.linearVelocity = velocity;
    }

}

