using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> _keyValueList = new();

    public void OnBeforeSerialize()
    {
        if (_keyValueList.Count != Count) return;

        _keyValueList.Clear();

        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            _keyValueList.Add(new SerializableKeyValuePair<TKey, TValue>(pair.Key, pair.Value));
        }
    }
    
    public void OnAfterDeserialize()
    {
        Clear();

        foreach(SerializableKeyValuePair<TKey, TValue> pair in _keyValueList)
        {
            if (ContainsKey(pair.Key)) return;

            TryAdd(pair.Key, pair.Value);
        }
    }
}
