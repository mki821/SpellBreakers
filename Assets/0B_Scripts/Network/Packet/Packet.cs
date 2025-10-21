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

[MessagePackObject]
public class RegisterPacket : PacketBase
{
    [Key(1)] public string Nickname { get; set; } = "";
    [Key(2)] public string Password { get; set; } = "";

    public RegisterPacket() { ID = (ushort)PacketId.Register; }
}

[MessagePackObject]
public class RegisterResponsePacket : PacketBase
{
    [Key(1)] public bool Success { get; set; }
    [Key(2)] public string Message { get; set; } = "";

    public RegisterResponsePacket() { ID = (ushort)PacketId.RegisterResponse; }
}

[MessagePackObject]
public class LoginPacket : PacketBase
{
    [Key(1)] public string Nickname { get; set; } = "";
    [Key(2)] public string Password { get; set; } = "";

    public LoginPacket() { ID = (ushort)PacketId.Login; }
}

[MessagePackObject]
public class LoginResponsePacket : PacketBase
{
    [Key(1)] public bool Success { get; set; }
    [Key(2)] public string Message { get; set; } = "";
    [Key(3)] public string IssuedToken { get; set; } = "";

    public LoginResponsePacket() { ID = (ushort)PacketId.LoginResponse; }
}

[MessagePackObject]
public class ChatPacket : PacketBase
{
    [Key(1)] public string Sender { get; set; } = "";
    [Key(2)] public string Message { get; set; } = "";
    
    public ChatPacket() { ID = (ushort)PacketId.Chat; }
}

[MessagePackObject]
public class MovePacket : UdpPacketBase
{
    [Key(2)] public float X { get; set; }
    [Key(3)] public float Y { get; set; }

    public MovePacket() { ID = (ushort)PacketId.Move; }
}
