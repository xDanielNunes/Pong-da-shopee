using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";

    public void StartGame()
    {
        Time.timeScale = 1f; //garante que o jogo não está pausado
        SceneManager.LoadScene(gameSceneName);//carrega a cena do jogo em si (medo de trocar o nome e dar pau sei la)
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

