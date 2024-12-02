using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blackScreen;
    //[SerializeField]
    //private Animator blackScreenAnimator;

    [SerializeField]
    private float timeForBlackScreen;


    private void OnEnable()
    {
        EventManager.ChangeLevel += SetBlackScreen;
    }
    private void OnDisable()
    {
        EventManager.ChangeLevel -= SetBlackScreen;
    }

    private void SetBlackScreen()
    {
        Debug.Log("Set black Screen");
        blackScreen.SetActive(true);
        //blackScreenAnimator.SetTrigger("In");
        Invoke("DisableBlackScreen", timeForBlackScreen);
    }

    private void Start()
    {
        blackScreen.SetActive(false);
    }
    
    private void DisableBlackScreen()
    {
        Debug.Log("Disable black Screen");
        //blackScreenAnimator.SetTrigger("Out");
        blackScreen.SetActive(false);
        EventManager.FinishMovePlayer?.Invoke();
    }
}
