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
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Move)?.Invoke();
        else if (context.canceled) EventDictionary.GetValueOrDefault(InputType.MoveCancel)?.Invoke();
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill1)?.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill2)?.Invoke();
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill3)?.Invoke();
    }

    public void OnSkill4(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill4)?.Invoke();
    }

    public void OnSkill5(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill5)?.Invoke();
    }

    public void OnSkill6(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill6)?.Invoke();
    }

    public void OnSkill7(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill7)?.Invoke();
    }

    public void OnSkill8(InputAction.CallbackContext context)
    {
        if (context.performed) EventDictionary.GetValueOrDefault(InputType.Skill8)?.Invoke();
    }
}
