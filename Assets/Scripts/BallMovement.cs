using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    //inicia o movimento da bola em uma direção aleatória
    void LaunchBall()
    {
        //decide uma direção aleatória para a bola
        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.5f, 0.5f);
        
        //aplica os valores de direção aleatórios e normaliza para evitar velocidades maiores na diagonal
        Vector2 direction = new Vector2(xDir, yDir).normalized;
        //aplica a velocidade em si no rigidbody da bola
        rb.linearVelocity = direction * speed;
    }
    
    //quando a bola colide com algo
    void OnCollisionEnter2D(Collision2D collision)
    {
        //se bateu no paddle (verifica a tag)
        if (collision.gameObject.CompareTag("Paddle"))
        {
            //chama a função que lida com o rebote da bola no paddle
            HandlePaddleBounce(collision);
            //toca o som de batida
            gameManager.PlayHitSound();
        }
    }

    //quando a bola entra na área de gol
    void OnTriggerEnter2D(Collider2D collision)
    {
        //verifica se entrou no gol esquerdo ou direito, adiciona ponto ao paddle oposto e reseta a bola
        if (collision.CompareTag("GolLeft"))
        {
            gameManager.PointRightPaddle();
            ResetBall();
        }
        else if (collision.CompareTag("GolRight"))
        {
            gameManager.PointLeftPaddle();
            ResetBall();
        }
    }


    //lida com o rebote da bola no paddle
    void HandlePaddleBounce(Collision2D collision)
    {
        //diferença de altura entre bola e paddle
        float paddleY = collision.transform.position.y;
        float ballY = transform.position.y;
        
        //quanto mais longe do centro, mais inclinado o rebote
        float hitFactor = (ballY - paddleY);

        //redirecionamento horizontal depende de qual paddle bateu
        float xDir = collision.transform.position.x < 0 ? 1 : -1;
        
        //nova direção normalizada
        Vector2 direction = new Vector2(xDir, hitFactor).normalized;
        rb.linearVelocity = direction * speed;
    }

    //reseta a posição e movimento da bola quando um ponto é marcado
    public void ResetBall()
    {
        //para a bola
        rb.linearVelocity = Vector2.zero;

        //volta para o centro
        transform.position = Vector2.zero;

        // relança a bola após um pequeno delay chamando a função LaunchBall
        Invoke(nameof(LaunchBall), 0.5f);
    }



}