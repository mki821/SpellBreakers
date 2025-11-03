using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Toggle _privateToggle;
    [SerializeField] private TMP_InputField _passwordInput;

    public void Create()
    {
        CreateRoomPacket packet = new CreateRoomPacket
        {
            Name = _nameInput.text,
            Password = _privateToggle ? _passwordInput.text : string.Empty
        };

        NetworkManager.Instance.SendAsync(packet);

        gameObject.SetActive(false);
    }

    public void Interactable(bool isOn) => _passwordInput.interactable = !isOn;
}
