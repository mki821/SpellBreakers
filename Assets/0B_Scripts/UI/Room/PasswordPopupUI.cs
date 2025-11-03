using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class PasswordPopupUI : PopupUI
{
    [SerializeField] private TMP_InputField _passwordInput;

    private TaskCompletionSource<string> _tcs;

    public override void Initialize() { }
    public override void Close() { }

    public Task<string> GetPassword()
    {
        _tcs = new TaskCompletionSource<string>();

        return _tcs.Task;
    }

    public void Submit()
    {
        _tcs.TrySetResult(_passwordInput.text);
    }
}
