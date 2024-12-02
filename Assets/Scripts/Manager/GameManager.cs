using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GAMEOVER,
    WIN,
    PAUSE,
    GAMEPLAY,
    TRANSITION
}
public class GameManager : Singleton<GameManager>
{

    private GameState state;


    private void OnEnable()
    {
        EventManager.ChangeLevel += Transition;
        EventManager.FinishMovePlayer += Resume;
        EventManager.PauseCall += PauseEvent;
    }

    private void OnDisable()
    {
        EventManager.ChangeLevel -= Transition;
        EventManager.FinishMovePlayer -= Resume;
        EventManager.PauseCall -= PauseEvent;
    }

    private void Resume()
    {
        ChangeState(GameState.GAMEPLAY);
    }

    private void Start()
    {
        state = GameState.GAMEPLAY;
        AudioManager.Instance.PlaySoundtrack("MAIN");
    }

    public void ChangeState(GameState currentState)
    {
        state = currentState;

        switch (state)
        {
            case GameState.GAMEOVER:
                EventManager.StateGameOver?.Invoke();
                StartCoroutine(WaitLoadScene("GameOver"));
                break;
            case GameState.WIN:
                StartCoroutine(WaitLoadScene("WinScreen"));
                break;
            case GameState.GAMEPLAY:
                EventManager.StateGamePlay?.Invoke();
                Time.timeScale = 1;
                break;
            case GameState.PAUSE:
                EventManager.StatePause?.Invoke();
                Time.timeScale = 0;
                break;
            default:
                print(currentState);
                break;
        }
    }

    public GameState SeeCurrentState()
    {
        return state;
    }

    private void Transition()
    {
        ChangeState(GameState.TRANSITION);
    }

    private void PauseEvent(GameState status)
    {
        print("receive input to change status");
        ChangeState(status);
    }

    IEnumerator WaitLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
