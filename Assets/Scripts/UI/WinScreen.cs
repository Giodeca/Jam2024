using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] float winScreenDuration;

    IEnumerator Start()
    {
        AudioManager.Instance.StopSoundtrack();
        AudioManager.Instance.PlaySFX("LAUGH");
        yield return new WaitForSeconds(winScreenDuration);
        SceneManager.LoadScene("Start Menu");
    }
}
