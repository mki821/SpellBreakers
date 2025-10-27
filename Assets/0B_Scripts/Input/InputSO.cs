using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "SO/Input")]
public class InputSO : ScriptableObject, Input.IPlayerActions
{
    public Vector2 MousePosition { get; private set; }
    public Vector2 Movement { get; private set; }
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

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }
}
