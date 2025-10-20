using UnityEngine;
using TMPro;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    public void Login()
    {
        LoginPacket packet = new LoginPacket
        {
            Nickname = _nicknameInput.text,
            Password = _passwordInput.text
        };

        NetworkManager.Instance.SendAsync(packet);
    }
}
