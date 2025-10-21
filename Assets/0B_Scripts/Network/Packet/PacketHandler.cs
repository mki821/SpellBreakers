using System;
using System.Collections.Generic;
using UnityEngine;

public static class PacketHandler
{
    private static readonly Dictionary<PacketId, Action<PacketBase>> _handlers = new Dictionary<PacketId, Action<PacketBase>>();

    public static void Register(PacketId id, Action<PacketBase> handler)
    {
        _handlers[id] = handler;
    }

    public static Action Handle(PacketId id, PacketBase packet)
    {
        if (_handlers.TryGetValue(id, out Action<PacketBase> handler))
        {
            return () => handler?.Invoke(packet);
        }

        Debug.LogWarning($"{id} 없음.");

        return null;
    }
}
