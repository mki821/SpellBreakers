using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RoomElementUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _lockedIcon;
    [SerializeField] private Sprite _unlockedSprite;
    [SerializeField] private Sprite _lockedSprite;

    [SerializeField] private TextMeshProUGUI _roomNameText;

    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private TextMeshProUGUI _spectatorCountText;

    [SerializeField] private Image _playingIcon;
    [SerializeField] private Color _notPlayingColor;
    [SerializeField] private Color _playingColor;

    private ushort _id;
    private bool _locked;

    public void Initialize(RoomElement element)
    {
        _locked = element.Locked;
        _lockedIcon.sprite = _locked ? _lockedSprite : _unlockedSprite;
        _roomNameText.text = element.Name;
        _playerCountText.text = $"{element.Players}/2";
        _spectatorCountText.text = $"{element.Spectators}/4";
        _playingIcon.color = element.Playing ? _playingColor : _notPlayingColor;

        _id = element.ID;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        JoinRoomPacket packet = new JoinRoomPacket();
        packet.RoomID = _id;

        if (_locked)
        {
            PasswordPopupUI popup = (PasswordPopupUI)UIManager.Instance.PopupUI.AddPopup(PopupType.Password);

            string password = await popup.GetPassword();
            packet.Password = password;

            UIManager.Instance.PopupUI.RemovePopup();
        }

        NetworkManager.Instance.SendAsync(packet);
    }
}
