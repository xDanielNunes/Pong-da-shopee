using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    public Transform ball;    
    public float speed = 6f;   
    public float deadZone = 0.1f; // pra regular a tremedeira
    public Rigidbody2D ballRb;
    public float reactionTime = 0.1f; // tempo de reação da IA do paddle


    private Rigidbody2D rb;
    private float reactionTimer; // temporizador para o tempo de reação

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float currentDirection = 0f; // direção atual do paddle
    private float targetY; // posição y alvo da bola

    void FixedUpdate()
    {

        //se a bola não vem em direção ao paddle a IA não se move
        if (ballRb.linearVelocity.x <= 0)
        {
            currentDirection = 0;
        }
        else
        {
            //atualiza o temporizador de reação
            reactionTimer -= Time.fixedDeltaTime;

            if (reactionTimer <= 0 && ball != null)
            {
                //reseta o temporizador de reação
                reactionTimer = reactionTime;
                //atualiza a posição y alvo da bola
                targetY = ball.position.y;
            }

            //calcula a diferença entre a posição y do paddle e da bola
            float delta = targetY - transform.position.y;

            //decide a direção do movimento baseado no delta e na deadzone
            if (Mathf.Abs(delta) < deadZone)
                currentDirection = 0f;
            else
                currentDirection = Mathf.Sign(delta);
        }

        //movimenta o paddle continuamente na direção atual
        rb.linearVelocity = new Vector2(0, currentDirection * speed);


        //MESMA LÓGICA DE IMPEDIR O PADDLE DE SAIR DA TELA LÁ DO PaddleInput.cs

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
