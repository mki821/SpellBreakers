using UnityEngine;
using TMPro;

public class WarningPopupUI : PopupUI
{
    [SerializeField] private TextMeshProUGUI _messageText;

    public override void Initialize() { }
    public override void Close() { }

    public void SetText(string message)
    {
        _messageText.text = message;
    }

    public void Submit()
    {
        UIManager.Instance.PopupUI.RemovePopup();
    }
}
