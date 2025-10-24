using MessagePack;

[MessagePackObject]
public class PacketBase
{
    [Key(0)] public virtual ushort ID { get; set; }
}

[MessagePackObject]
public class UdpPacketBase : PacketBase
{
    [Key(1)] public string Token { get; set; } = "";
}
