using UnityEngine;
using TMPro;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    public void Create()
    {
        CreateRoomPacket packet = new CreateRoomPacket();
        packet.Name = _nameInput.text;
        packet.Password = _passwordInput.text;

        NetworkManager.Instance.SendAsync(packet);

        gameObject.SetActive(false);
    }
}
