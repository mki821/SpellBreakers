using UnityEngine;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _messageInput;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private ChatElementUI _prefab;

    public void UpdateChat(PacketBase packet)
    {
        ChatPacket chat = (ChatPacket)packet;

        if (string.IsNullOrEmpty(chat.Sender))
        {
            AddChat(chat.Message);
        }
        else
        {
            AddChat($"[{chat.Sender}] {chat.Message}");
        }
    }

    private void AddChat(string message)
    {
        Instantiate(_prefab, _contentTransform).SetText(message);
    }

    public void ClearChat()
    {
        foreach (Transform text in _contentTransform)
        {
            Destroy(text.gameObject);
        }
        
        _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, 0.0f);
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
