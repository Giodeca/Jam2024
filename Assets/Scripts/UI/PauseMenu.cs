using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Button resumeButton;

    private void OnEnable() {
        resumeButton.Select();
    }

    public void ClickResume() {
        EventManager.PauseCall?.Invoke(GameState.GAMEPLAY);
    }

    public void ClickQuitToMenu() {
        SceneManager.LoadScene("Start Menu");
    }
}
