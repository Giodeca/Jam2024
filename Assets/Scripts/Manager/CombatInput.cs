using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CombatInput : MonoBehaviour
{
    private PlayerHealth player;

    InputMovement input;
    int attackInutCount = 0;
    bool coroutineStarted;

    private void Awake()
    {
        input = new InputMovement();
        player = GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        input.Movement.Attack.performed += ctx => AttackPerformed();
        input.Movement.Perry.performed += ctx => ParryPerformed(ctx);
        input.Movement.Pick.performed += ctx => PickPerfomed(ctx);
        input.Movement.Throw.performed += ctx => ThrowPerfomed(ctx);
        input.Movement.Pause.performed += ctx => PausePerformed(ctx);
        input.Movement.Immortal.performed += ctx => ImmortalEnable(ctx);
        input.Movement.Normal.performed += ctx => ImmortalDisable(ctx);
    }

    private void ImmortalDisable(InputAction.CallbackContext ctx)
    {
        player.Normal();
    }

    private void ImmortalEnable(InputAction.CallbackContext ctx)
    {
        player.Immortal();

    }

    private void PausePerformed(InputAction.CallbackContext ctx)
    {
        print(GameManager.Instance.SeeCurrentState());
        if(GameManager.Instance.SeeCurrentState() != GameState.GAMEOVER)
        {
            if (GameManager.Instance.SeeCurrentState() != GameState.PAUSE)
            {
                print("pause command");
                EventManager.PauseCall?.Invoke(GameState.PAUSE);
            }
            else
            {
                print("gameplay command");
                EventManager.PauseCall?.Invoke(GameState.GAMEPLAY);
            }
        }


    }

    private void ThrowPerfomed(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.SeeCurrentState() != GameState.PAUSE)
        {
            EventManager.ThrowAction?.Invoke();
        }
        
    }

    private void PickPerfomed(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.SeeCurrentState() != GameState.PAUSE)
        {
            EventManager.PickAction?.Invoke();
        }


    }

    private void AttackPerformed()
    {
        if(GameManager.Instance.SeeCurrentState() != GameState.PAUSE && GameManager.Instance.SeeCurrentState() != GameState.GAMEOVER)
        {
            EventManager.AttackPerformed?.Invoke();
            attackInutCount++;
            if (!coroutineStarted)
                StartCoroutine(AttackCombo());
        }

    }

    IEnumerator AttackCombo()
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(0.4f);
        if(attackInutCount == 1)
        {
            EventManager.FirstAttackPerformed?.Invoke();
            attackInutCount = 0;
            coroutineStarted = false;
        }
        else if(attackInutCount > 1)
        {
            EventManager.ComboAttackPerformed?.Invoke();
            attackInutCount = 0;
            coroutineStarted = false;
        }
    }

    private void ParryPerformed(InputAction.CallbackContext action)
    {
        if (GameManager.Instance.SeeCurrentState() != GameState.PAUSE)
        {
            EventManager.ParryAction?.Invoke();
        }

    }
}
