using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputType : byte
{
    Move,
}

public class InputManager : MonoSingleton<InputManager>
{
    public event Action RebindEndEvent;

    [SerializeField] private InputSO _so;

    public void AddListener(InputType type, Action callback)
    {
        if (!_so.EventDictionary.ContainsKey(type))
        {
            _so.EventDictionary.Add(type, callback);
        }
        else
        {
            _so.EventDictionary[type] += callback;
        }
    }

    public void RemoveListener(InputType type, Action callback) => _so.EventDictionary[type] -= callback;
    public Vector2 GetMovement() => _so.Movement;

    public void Rebind(InputType type, bool mouseEnable = true)
    {
        _so.CustomInput.Player.Disable();

        InputAction inputAction = InputTypeToInputAction(type);
        InputActionRebindingExtensions.RebindingOperation operation = inputAction.PerformInteractiveRebinding();

        if ((int)type < 4)
            operation.WithTargetBinding((int)type + 1).Start();

        if (mouseEnable)
            operation.WithControlsExcluding("Mouse");

        operation.WithCancelingThrough("<keyboard>/escape")
            .OnComplete(op =>
            {
                op.Dispose();
                RebindEndEvent?.Invoke();
                _so.CustomInput.Player.Enable();
            }).OnCancel(op =>
            {
                op.Dispose();
                _so.CustomInput.Player.Enable();
            }).Start();
    }

    public string GetCurrentKeyByType(InputType type)
    {
        InputAction inputAction = InputTypeToInputAction(type);

        if ((int)type < 4)
            return inputAction.GetBindingDisplayString((int)type + 1);

        return inputAction.GetBindingDisplayString();
    }

    private InputAction InputTypeToInputAction(InputType type)
    {
        return type switch
        {
            InputType.Move => _so.CustomInput.Player.Move,
            _ => default,
        };
    }
}
