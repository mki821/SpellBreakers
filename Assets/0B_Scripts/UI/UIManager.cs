using UnityEngine;

[MonoSingletonUsage(MonoSingletonFlags.None)]
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SerializableDictionary<UIType, UIBase> _uis;

    [SerializeField] private SerializableDictionary<PopupType, PopupUI> _popupUis;
    [SerializeField] private Transform _popupTransform;
    public PopupUIManager PopupUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PopupUI = new PopupUIManager(_popupUis, _popupTransform);
    }

    public UIBase GetUI(UIType type)
    {
        if(_uis.TryGetValue(type, out UIBase ui))
        {
            return ui;
        }

        return null;
    }
}
