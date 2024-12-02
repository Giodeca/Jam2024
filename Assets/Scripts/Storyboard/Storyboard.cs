using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Storyboard : MonoBehaviour
{
    [SerializeField] StoryboardScene[] scenes;
    [SerializeField] Image storyboardImage;
    public string soundtrack;

    void Start()
    {
        AudioManager.Instance.StopSoundtrack();
        AudioManager.Instance.PlaySoundtrack(soundtrack);
        StartCoroutine(StoryboardFlow());
    }

    private IEnumerator StoryboardFlow()
    {
        foreach (StoryboardScene scene in scenes)
        {
            storyboardImage.sprite = scene.image;
            if (scene.sfx != "") AudioManager.Instance.PlaySFX(scene.sfx);
            yield return new WaitForSeconds(scene.duration);
        }
        AudioManager.Instance.StopSoundtrack();
        SceneManager.LoadScene("GameplayScene");
    }
}

[Serializable]
public struct StoryboardScene
{
    public Sprite image;
    public float duration;
    public string sfx;
}