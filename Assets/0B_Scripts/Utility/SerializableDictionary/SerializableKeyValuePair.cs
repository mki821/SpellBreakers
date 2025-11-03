using System;
using UnityEngine;

[Serializable]
public struct SerializableKeyValuePair<TKey, TValue>
{
    [SerializeField] private TKey _key;
    [SerializeField] private TValue _value;

    public readonly TKey Key => _key;
    public readonly TValue Value => _value;
    
    public SerializableKeyValuePair(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }
}