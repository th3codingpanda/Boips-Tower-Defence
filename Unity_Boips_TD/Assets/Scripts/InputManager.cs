using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
        
    [NonSerialized]public readonly UnityEvent<Vector2> CameraEvent = new UnityEvent<Vector2>();
    [NonSerialized]public readonly UnityEvent<Vector2> MoveEvent = new UnityEvent<Vector2>();
    [NonSerialized]public readonly UnityEvent JumpEvent = new UnityEvent();
    [NonSerialized]public readonly UnityEvent PlaceTowerEvent = new UnityEvent();
    [NonSerialized]public readonly UnityEvent<float> SwapTowerEvent = new UnityEvent<float>();
    [NonSerialized]public readonly UnityEvent AttackEvent = new UnityEvent();
    [NonSerialized]public readonly UnityEvent StartRound = new UnityEvent();
    [NonSerialized]public readonly UnityEvent PauzeGame = new UnityEvent();


    InputAction _cameraAction;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _placeTowerAction;
    InputAction _swapTowerAction;
    InputAction _attackAction;
    InputAction _startRoundAction;
    InputAction _pauzeGameAction;



    void Start()
    {
        _cameraAction = InputSystem.actions.FindAction("Camera");
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _placeTowerAction = InputSystem.actions.FindAction("PlaceTower");
        _swapTowerAction = InputSystem.actions.FindAction("SwapTower");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _startRoundAction = InputSystem.actions.FindAction("StartRound");
        _pauzeGameAction = InputSystem.actions.FindAction("PauzeGame");
    }
    void Update()
    {

        if (_cameraAction.IsInProgress())
        {
            CameraEvent.Invoke(_cameraAction.ReadValue<Vector2>());
            
        }
        if (_jumpAction.IsInProgress())
        {
            JumpEvent.Invoke();
        }
        MoveEvent.Invoke(_moveAction.ReadValue<Vector2>());
        if (_placeTowerAction.triggered)
        {
            PlaceTowerEvent.Invoke();
        }

        if (_swapTowerAction.triggered)
        {
            SwapTowerEvent.Invoke(_swapTowerAction.ReadValue<float>());    
        }
        if (_attackAction.triggered)
        {
            AttackEvent.Invoke();
        }
        if (_startRoundAction.triggered)
        {
            StartRound.Invoke();
        }
        if (_pauzeGameAction.triggered)
        {
            PauzeGame.Invoke();
        }
    }

}