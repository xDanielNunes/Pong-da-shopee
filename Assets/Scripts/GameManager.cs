using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
    
public class GameManager : MonoBehaviour
{
    //pontuações dos paddles
    public int leftPaddleScore = 0;
    public int rightPaddleScore = 0;

    //referências para os textos de pontuação na UI
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;

    //painéis de fim de jogo
    public GameObject endGamePanelWin;
    public GameObject endGamePanelLose;
    public GameObject Background;

    private bool gameEnded = false; //indica se o jogo acabou

    public int maxScore = 5; //pontuação máxima para vencer
    
    private PongControls controls;

    //efeitos sonoros
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip scoreSound;

    //configuração do input system para reiniciar o jogo
    void Awake()
    {
        controls = new PongControls();
    }

    void OnEnable()
    {
        //ativa os controles e associa a ação de reiniciar jogo
        controls.Enable();
        controls.Player.Restart.performed += OnRestart;
    }

    void OnDisable()
    {
        //desativa os controles e remove a associação da ação de reiniciar jogo
        controls.Player.Restart.performed -= OnRestart;
        controls.Disable();
    }
    
    //atualiza os textos de pontuação na UI inicialmente
    void Start()
    {
        //pega a referência do audio source
        audioSource = GetComponent<AudioSource>();
        //atualiza a UI com as pontuações iniciais
        UpdateUI();
    } 

    //funções para adicionar pontos aos paddles, mostrar na tela, tocar o som e checar vitória
    public void PointLeftPaddle()
    {
        leftPaddleScore++;
        PlayScoreSound();
        UpdateUI();
        CheckVictory();
    }

    public void PointRightPaddle()
    {
        rightPaddleScore++;
        PlayScoreSound();
        UpdateUI();
        CheckVictory();
    }

    //atualiza os textos de pontuação na UI
    public void UpdateUI()
    {
        leftScoreText.text = leftPaddleScore.ToString();
        rightScoreText.text = rightPaddleScore.ToString();
    }

    //verifica se algum paddle atingiu a pontuação máxima para vencer
    void CheckVictory()
    {
        if (leftPaddleScore >= maxScore)
            EndGame(true);
        else if (rightPaddleScore >= maxScore)
            EndGame(false);
    }

    //lida com o fim do jogo, mostrando o painel apropriado
    void EndGame(bool win)
    {
        //define que o jogo acabou e pausa o tempo
        gameEnded = true;
        Time.timeScale = 0f;

        if (win)
        {
            Background.SetActive(true);
            endGamePanelWin.SetActive(true);
        }
        else
        {
            Background.SetActive(true);
            endGamePanelLose.SetActive(true);
        }    
    }

    //função chamada pelo input system para reiniciar o jogo
    void OnRestart(InputAction.CallbackContext context)
    {
        //só reinicia se o jogo já tiver acabado
        if (!gameEnded)
            return;

        RestartGame();
    }


    //função para reiniciar o jogo
    public void RestartGame()
    {
        Time.timeScale = 1f; // reseta o tempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // recarrega a cena atual
    }

    //função para tocar efeitos sonoros
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    //função para tocar o som de pontuação
    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    //função para tocar o som de pontuação
    public void PlayScoreSound()
    {
        PlaySound(scoreSound);
    }

}