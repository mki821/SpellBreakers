using UnityEngine;

[MonoSingletonUsage(MonoSingletonFlags.None)]
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private PopupUIList _popupUIList;
    [SerializeField] private Transform _popupTransform;
    public PopupUIManager PopupUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PopupUI = new PopupUIManager(_popupUIList, _popupTransform);
    }
}
