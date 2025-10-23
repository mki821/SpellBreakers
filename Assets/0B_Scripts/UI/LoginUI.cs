using UnityEngine;
using TMPro;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private GameObject _temp;

    private void Awake()
    {
        PacketHandler.Register(PacketId.LoginResponse, CompleteLogin);
    }

    private void CompleteLogin(PacketBase packet)
    {
        LoginResponsePacket response = (LoginResponsePacket)packet;

        if (response.Success)
        {
            ListRoomPacket list = new ListRoomPacket();
            NetworkManager.Instance.SendAsync(list);
            
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

    public void ChangeRegister()
    {
        _temp.SetActive(true);
        gameObject.SetActive(false);
    }
}
