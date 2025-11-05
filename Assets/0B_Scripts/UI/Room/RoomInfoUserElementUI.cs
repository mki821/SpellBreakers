using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class RoomInfoUserElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private GameObject _readyIcon;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetInfo(UserElement element)
    {
        bool isActive = element != null;

        _nicknameText.text = isActive ? element.Nickname : "비어있음";
        _nicknameText.color = new Color(_nicknameText.color.r, _nicknameText.color.g, _nicknameText.color.b, isActive ? 1.0f : 0.5f);

        _readyIcon.SetActive(isActive && element.IsReady);
            
        _image.color = isActive ? _activeColor : _inactiveColor;
    }
}
