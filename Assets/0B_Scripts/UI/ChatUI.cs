using UnityEngine;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _messageInput;
    [SerializeField] private TextMeshProUGUI _chatText;

    private void Awake()
    {
        PacketHandler.Register(PacketId.Chat, UpdateChat);
    }
    
    private void UpdateChat(PacketBase packet)
    {
        ChatPacket chat = (ChatPacket)packet;

        if (string.IsNullOrEmpty(chat.Sender))
        {
            _chatText.text += $"\n{chat.Message}";
        }
        else
        {
            _chatText.text += $"\n[{chat.Sender}] {chat.Message}";
        }
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
