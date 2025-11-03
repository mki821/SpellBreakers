using UnityEngine;
using TMPro;

public class RegisterUI : UIBase
{
    [SerializeField] private TMP_InputField _idInput;
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_InputField _checkPasswordInput;

    public void Submit()
    {
        if (_passwordInput.text != _checkPasswordInput.text)
        {
            WarningPopupUI warning = UIManager.Instance.PopupUI.AddPopup(PopupType.Warning) as WarningPopupUI;
            warning.SetText("비밀번호가 서로 다릅니다!");

            return;
        }
        
        RegisterPacket packet = new RegisterPacket
        {
            UserID = _idInput.text,
            Nickname = _nicknameInput.text,
            Password = _passwordInput.text
        };

        NetworkManager.Instance.SendAsync(packet);
    }

    public void Change()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GetUI(UIType.Login).gameObject.SetActive(true);
    }
}
