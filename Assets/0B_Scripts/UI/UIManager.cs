using UnityEngine;

[MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SerializableDictionary<UIType, UIBase> _ui;

    [SerializeField] private SerializableDictionary<PopupType, PopupUI> _popupUI;
    [SerializeField] private Transform _popupTransform;
    public PopupUIManager PopupUI { get; private set; }

    [Space]

    [SerializeField] private SerializableDictionary<HUDType, HUDUI> _hudUI;
    [SerializeField] private Transform _hudTransform;
    public HUDUIManager HUDUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PopupUI = new PopupUIManager(_popupUI, _popupTransform);
        HUDUI = new HUDUIManager(_hudUI, _hudTransform);
    }

    public UIBase GetUI(UIType type)
    {
        if(_ui.TryGetValue(type, out UIBase ui))
        {
            return ui;
        }

        return null;
    }
}
