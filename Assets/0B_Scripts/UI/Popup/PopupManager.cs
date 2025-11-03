using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager
{
    private Stack<PopupUI> _popupStack;
    private Dictionary<PopupType, PopupUI> _popupDictionary;
    private Transform _popupTransform;

    public PopupUIManager(Dictionary<PopupType, PopupUI> popupDictionary, Transform popupTransform)
    {
        _popupStack = new Stack<PopupUI>();
        _popupDictionary = popupDictionary;
        _popupTransform = popupTransform;
    }

    public PopupUI AddPopup(PopupType type)
    {
        if (!_popupDictionary.TryGetValue(type, out PopupUI popup))
            return null;

        PopupUI newPopup = Object.Instantiate(popup, _popupTransform);
        newPopup.Initialize();

        _popupStack.Push(newPopup);

        return newPopup;
    }

    public void RemovePopup()
    {
        if(_popupStack.Count > 0)
        {
            PopupUI popup = _popupStack.Pop();
            popup.Close();
            Object.Destroy(popup.gameObject);
        }
    }
}
