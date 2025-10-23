using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager
{
    private Stack<PopupUI> _popupStack;
    private Dictionary<PopupType, PopupUI> _popupDictionary;
    private Transform _popupTransform;

    public PopupUIManager(PopupUIList list, Transform popupTransform)
    {
        _popupStack = new Stack<PopupUI>();
        _popupDictionary = new Dictionary<PopupType, PopupUI>();
        _popupTransform = popupTransform;

        List<PopupUIElement> llist = list.list;
        for (int i = 0; i < llist.Count; ++i)
        {
            _popupDictionary.Add(llist[i].type, llist[i].prefab);
        }
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
        PopupUI popup = _popupStack.Pop();
        popup.Close();
        Object.Destroy(popup.gameObject);
    }
}
