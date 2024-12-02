using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    InputMovement input;

    private void Awake()
    {
        input = new InputMovement();
    }
    private void OnEnable()
    {
        input.Disable();
    }
    public void ClickRestart()
    {

    }

    public void ClickQuitToMenu()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void QuitApp()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
