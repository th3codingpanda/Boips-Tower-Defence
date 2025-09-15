using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputPackageManagerScript : MonoSingleton<InputPackageManagerScript>
{
        
    [NonSerialized]public readonly UnityEvent<Vector2> CameraEvent = new UnityEvent<Vector2>();
    [NonSerialized]public readonly UnityEvent<Vector2> MoveEvent = new UnityEvent<Vector2>();
    [NonSerialized]public readonly UnityEvent JumpEvent = new UnityEvent();
    [NonSerialized]public readonly UnityEvent BuildMenuEvent = new UnityEvent();
    [NonSerialized]public readonly UnityEvent PlaceTowerEvent = new UnityEvent();
    
   
    InputAction _cameraAction;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _BuildMenuAction;
    InputAction _PlaceTowerAction;
  
        
    void Start()
    {
        Debug.Log("InputManager Start");
        _cameraAction = InputSystem.actions.FindAction("Camera");
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _BuildMenuAction = InputSystem.actions.FindAction("BuildMenu");
        _PlaceTowerAction = InputSystem.actions.FindAction("PlaceTower");
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
        if (_BuildMenuAction.triggered)
        {
            BuildMenuEvent.Invoke();
        }

        if (_PlaceTowerAction.triggered)
        {
            PlaceTowerEvent.Invoke();
        }


    }

}