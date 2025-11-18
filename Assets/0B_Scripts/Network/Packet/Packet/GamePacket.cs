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
[Union(0, typeof(DefaultEntityInfo))]
[Union(1, typeof(CharacterInfo))]
public abstract class EntityInfo
{
    [Key(0)] public ushort EntityType { get; set; }
    [Key(1)] public string EntityID { get; set; } = "";
    [Key(2)] public Vector Position { get; set; }
    [Key(3)] public bool IsMoving { get; set; }
}

[MessagePackObject]
public class DefaultEntityInfo : EntityInfo
{

}

[MessagePackObject]
public class CharacterInfo : EntityInfo
{
    [Key(4)] public ushort CharacterType { get; set; }
    [Key(5)] public float CurrentHealth { get; set; }
    [Key(6)] public float CurrentMana { get; set; }
    [Key(7)] public Stat Stat { get; set; }
}

[MessagePackObject]
public struct Stat
{
    [Key(0)] public float MaxHealth { get; set; }
    [Key(1)] public float MaxMana { get; set; }
    [Key(2)] public float Force { get; set; }
    [Key(3)] public float Resistance { get; set; }
    [Key(4)] public float Speed { get; set; }
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
public class SkillPacket : UdpPacketBase
{
    [Key(3)] public ushort SkillType { get; set; }
    [Key(4)] public string OwnerID { get; set; } = "";
    [Key(5)] public Vector SpawnPosition { get; set; }
    [Key(6)] public Vector TargetPosition { get; set; }
    
    public SkillPacket() { ID = (ushort)PacketId.Skill; }
}
