using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomElementUI : MonoBehaviour
{
    [SerializeField] private Image _lockedIcon;
    [SerializeField] private Sprite _unlockedSprite;
    [SerializeField] private Sprite _lockedSprite;

    [SerializeField] private TextMeshProUGUI _roomNameText;

    [SerializeField] private Image _playingIcon;
    [SerializeField] private Color _notPlayingColor;
    [SerializeField] private Color _playingColor;

    public void Initialize(RoomElement element)
    {
        _lockedIcon.sprite = element.Locked ? _lockedSprite : _unlockedSprite;
        _roomNameText.text = element.Name;
        _playingIcon.color = element.Playing ? _playingColor : _notPlayingColor;
    }
}
