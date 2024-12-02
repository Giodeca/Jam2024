using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private LevelData levelData;

    private void OnEnable()
    {
        startButton.Select();
    }

    private void Start()
    {
        AudioManager.Instance.StopSoundtrack();
        AudioManager.Instance.PlaySoundtrack("MENU");
    }

    public void ClickStart()
    {
        //levelData.LoadLastLevel();


        SceneManager.LoadScene("Storyboard");

    }

    public void AudioManagerMethod()
    {
        AudioManager.Instance.PlayUISFX("CLICK");
    }

    public void ClickQuit()
    {
        Application.Quit();
    }
}
