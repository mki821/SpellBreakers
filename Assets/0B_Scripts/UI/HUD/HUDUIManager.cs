using System.Collections.Generic;
using UnityEngine;

public class HUDUIManager
{
    private readonly Dictionary<string, HUDUI> _huds;
    private readonly Dictionary<HUDType, HUDUI> _hudDictionary;
    private readonly Transform _hudTransform;

    public HUDUIManager(Dictionary<HUDType, HUDUI> hudDictionary, Transform hudTransform)
    {
        _huds = new Dictionary<string, HUDUI>();
        _hudDictionary = hudDictionary;
        _hudTransform = hudTransform;
    }

    public void AddHUD(string id, HUDType type)
    {
        HUDUI ui = Object.Instantiate(_hudDictionary[type], _hudTransform);
        _huds.Add(id, ui);
    }

    public T GetUI<T>(string id) where T : HUDUI
    {
        if (_huds.TryGetValue(id, out HUDUI ui))
        {
            return ui as T;
        }

        return null;
    }
    
    public void RemoveHUD(string id)
    {
        if (_huds.TryGetValue(id, out HUDUI ui))
        {
            Object.Destroy(ui.gameObject);
            _huds.Remove(id);
        }
    }
}
