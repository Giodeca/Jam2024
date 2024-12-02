using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EventManager
{
    public static Action AttackPerformed;
    public static Action FirstAttackPerformed;
    public static Action ComboAttackPerformed;
    public static Action AttackAnimator;
    public static Action<float> MovePlayerAnimator;


    public static Action ParryAction;
    public static Action ParryAnimator;

    public static Action ReceiveDamage;

    public static Action PickAction;
    public static Action ThrowAction;
    public static Action<GameObject> ReferenceObjectPick;

    public static Action LessEnemie;



    public static Action<GameState> PauseCall;

    #region UI EVENTS
    public static Action StatePause;
    public static Action StateGamePlay;
    public static Action StateGameOver;
    public static Action ChangeLevel;
    public static Action FinishMovePlayer;
    #endregion

}
