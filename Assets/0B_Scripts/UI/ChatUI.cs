using UnityEngine;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _messageInput;
    [SerializeField] private TextMeshProUGUI _chatText;

    private void Awake()
    {
        PacketHandler.Register(PacketId.Chat, UpdateChat);
        PacketHandler.Register(PacketId.RoomInfo, (packet) => { int a = 0; });
    }
    
    private void UpdateChat(PacketBase packet)
    {
        ChatPacket chat = (ChatPacket)packet;
        _chatText.text += $"\n[{chat.Sender}] {chat.Message}";
    }

    public void Send()
    {
        ChatPacket packet = new ChatPacket
        {
            Message = _messageInput.text
        };

        _messageInput.text = "";

        NetworkManager.Instance.SendAsync(packet);
    }
}
