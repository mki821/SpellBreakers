using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class EntityInfoPacket : UdpPacketBase
{
    [Key(3)] public IEnumerable<EntityInfo> Entities;

    public EntityInfoPacket() { ID = (ushort)PacketId.EntityInfo; }
}

[MessagePackObject]
public class EntityInfo
{
    [Key(0)] public ushort EntityType { get; set; }
    [Key(1)] public string EntityID { get; set; }
    [Key(2)] public Vector Position { get; set; }
    [Key(3)] public bool IsMoving { get; set; }
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

    public Vector(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }

    public readonly Vector3 GetVector3() => new Vector3(X, Y, Z);
}

[MessagePackObject]
public class MovePacket : UdpPacketBase
{
    [Key(3)] public Vector TargetPosition { get; set; }

    public MovePacket() { ID = (ushort)PacketId.Move; }
}

[MessagePackObject]
public class FireProjectilePacket : UdpPacketBase
{
    [Key(3)] public string OwnerID { get; set; } = "";
    [Key(4)] public Vector SpawnPosition { get; set; }
    [Key(5)] public Vector TargetPosition { get; set; }
    
    public FireProjectilePacket() { ID = (ushort)PacketId.FireProjectile; }
}
