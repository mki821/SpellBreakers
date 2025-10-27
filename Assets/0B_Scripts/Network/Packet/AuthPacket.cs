using MessagePack;

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
public class UdpConnectPacket : UdpPacketBase
{
    public UdpConnectPacket() { ID = (ushort)PacketId.UdpConnect; }
}
