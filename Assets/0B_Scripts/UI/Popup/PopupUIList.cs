using System.Collections.Generic;
using UnityEngine;

public enum PopupType : ushort
{
    Password
}

[System.Serializable]
public struct PopupUIElement
{
    public PopupType type;
    public PopupUI prefab;
}

[CreateAssetMenu(fileName = "PopupUIList", menuName = "SO/PopupUIList")]
public class PopupUIList : ScriptableObject
{
    public List<PopupUIElement> list;
}
