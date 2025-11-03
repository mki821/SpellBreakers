using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class RoomInfoUserElementUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nicknameText;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetInfo(string nickname, bool isActive)
    {
        _nicknameText.text = nickname;
        _nicknameText.color = new Color(_nicknameText.color.r, _nicknameText.color.g, _nicknameText.color.b, isActive ? 1.0f : 0.5f);

        _image.color = isActive ? _activeColor : _inactiveColor;
    }
}
