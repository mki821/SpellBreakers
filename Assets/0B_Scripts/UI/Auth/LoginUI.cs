using UnityEngine;
using TMPro;

public class LoginUI : UIBase
{
    [SerializeField] private TMP_InputField _idInput;
    [SerializeField] private TMP_InputField _passwordInput;

    public void Submit()
    {
        LoginPacket packet = new LoginPacket
        {
            UserID = _idInput.text,
            Password = _passwordInput.text
        };

        NetworkManager.Instance.SendAsync(packet);
    }

    public void Change()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GetUI(UIType.Register).gameObject.SetActive(true);
    }
}
