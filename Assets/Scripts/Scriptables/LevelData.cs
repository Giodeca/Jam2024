using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject {
    [SerializeField] private List<string> sceneNames;

    public void LoadLastLevel() {
        int level = 0;
        if (PlayerPrefs.HasKey("Level")) {
            level = PlayerPrefs.GetInt("Level");
        }
        SceneManager.LoadScene(GetSceneName(level));
    }

    public string GetSceneName(int levelIndex) {
        return sceneNames[levelIndex];
    }
}
