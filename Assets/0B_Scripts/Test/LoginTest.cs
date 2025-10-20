using UnityEngine;

public class LoginTest : MonoBehaviour
{
    [SerializeField] private string _nickname;
    [SerializeField] private string _password;

    [ContextMenu("Register")]
    private void Register()
    {
        RegisterPacket packet = new RegisterPacket
        {
            Nickname = _nickname,
            Password = _password,
        };

        NetworkManager.Instance.SendAsync(packet);
    }

    [ContextMenu("Login")]
    private void Login()
    {
        LoginPacket packet = new LoginPacket
        {
            Nickname = _nickname,
            Password = _password,
        };

        NetworkManager.Instance.SendAsync(packet);
    }
}
