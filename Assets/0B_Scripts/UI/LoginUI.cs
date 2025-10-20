using UnityEngine;
using TMPro;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    private void Awake()
    {
        PacketHandler.Register(PacketId.LoginResponse, CompleteLogin);
    }

    private void CompleteLogin(PacketBase packet)
    {
        LoginResponsePacket response = (LoginResponsePacket)packet;

        if (response.Success)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(response.Message);
        }
    }

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
