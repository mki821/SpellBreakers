using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class EntityInfoPacket : UdpPacketBase
{
    [Key(2)] public IEnumerable<EntityInfo> Entities;

    public EntityInfoPacket() { ID = (ushort)PacketId.EntityInfo; }
}

[MessagePackObject]
public class EntityInfo
{
    [Key(0)] public ushort EntityType { get; set; }
    [Key(1)] public string EntityID { get; set; }
    [Key(2)] public Vector Position { get; set; }
}

[MessagePackObject]
public struct Vector
{
    [Key(0)] public float X;
    [Key(1)] public float Y;
    [Key(2)] public float Z;

    public Vector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public readonly Vector3 GetVector3() => new Vector3(X, Y, Z);
}

[MessagePackObject]
public class MovePacket : UdpPacketBase
{
    [Key(2)] public Vector TargetPosition { get; set; }

    public MovePacket() { ID = (ushort)PacketId.Move; }
}
