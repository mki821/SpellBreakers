using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "SO/Input")]
public class InputSO : ScriptableObject, Input.IPlayerActions
{
    public Vector2 MousePosition { get; private set; }
    public Dictionary<InputType, Action> EventDictionary = new Dictionary<InputType, Action>();

    private Input _customInput;

    public Input CustomInput => _customInput;

    private void OnEnable()
    {
        if (_customInput == null)
        {
            _customInput = new Input();
            _customInput.Player.SetCallbacks(this);
        }
        _customInput.Player.Enable();
    }

    private void OnDisable()
    {
        _customInput.Player.SetCallbacks(null);
        _customInput.Player.Disable();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary[InputType.Move]?.Invoke();
        else if (context.canceled) EventDictionary[InputType.MoveCancel]?.Invoke();
    }
}
