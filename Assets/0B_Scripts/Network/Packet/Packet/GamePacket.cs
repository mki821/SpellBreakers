using System.Collections.Generic;
using MessagePack;

[MessagePackObject]
public class EntityInfoPacket : UdpPacketBase
{
    [Key(2)] public List<EntityInfo> Entities;

    public EntityInfoPacket() { ID = (ushort)PacketId.EntityInfo; }
}

[MessagePackObject]
public class EntityInfo
{
    [Key(0)] public ushort EntityType { get; set; }
    [Key(1)] public string EntityID { get; set; }
    [Key(2)] public float X { get; set; }
    [Key(3)] public float Y { get; set; }
}

[MessagePackObject]
public class MovePacket : UdpPacketBase
{
    [Key(2)] public float X { get; set; }
    [Key(3)] public float Y { get; set; }

    public MovePacket() { ID = (ushort)PacketId.Move; }
}
