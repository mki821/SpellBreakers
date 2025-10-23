using UnityEngine;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private GameObject _temp;

    public void Register()
    {
        RegisterPacket packet = new RegisterPacket
        {
            Nickname = _nicknameInput.text,
            Password = _passwordInput.text
        };

        NetworkManager.Instance.SendAsync(packet);
    }

    public void ChangeLogin()
    {
        _temp.SetActive(true);
        gameObject.SetActive(false);
    }
}
