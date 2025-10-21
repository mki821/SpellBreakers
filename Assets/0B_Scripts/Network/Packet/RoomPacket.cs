using System.Collections.Generic;
using MessagePack;

[MessagePackObject]
public class ListRoomPacket : PacketBase
{
    public ListRoomPacket() { ID = (ushort)PacketId.ListRoom; }
}
[MessagePackObject]
public class ListRoomResponsePacket : PacketBase
{
    [Key(1)] public List<RoomElement> Rooms { get; set; } = new List<RoomElement>();

    public ListRoomResponsePacket() { ID = (ushort)PacketId.ListRoomResponse; }
}

[MessagePackObject]
public class RoomElement
{
    [Key(0)] public ushort ID { get; set; }
    [Key(1)] public string Name { get; set; } = "";
    [Key(2)] public bool Locked { get; set; }
    [Key(3)] public bool Playing { get; set; }
    [Key(4)] public ushort Players { get; set; }
    [Key(5)] public ushort Spectators { get; set; }
}

[MessagePackObject]
public class CreateRoomPacket : PacketBase
{
    [Key(1)] public string Name { get; set; } = "";
    [Key(2)] public string Password { get; set; } = "";

    public CreateRoomPacket() { ID = (ushort)PacketId.CreateRoom; }
}

[MessagePackObject]
public class JoinRoomPacket : PacketBase
{
    [Key(1)] public ushort RoomID { get; set; }
    [Key(2)] public string Password { get; set; } = "";

    public JoinRoomPacket() { ID = (ushort)PacketId.JoinRoom; }
}

[MessagePackObject]
public class JoinRoomResponsePacket : PacketBase
{
    [Key(1)] public bool Success { get; set; }
    [Key(2)] public string Message { get; set; } = "";

    public JoinRoomResponsePacket() { ID = (ushort)PacketId.JoinRoomResponse; }
}

[MessagePackObject]
public class LeaveRoomPacket : PacketBase
{
    public LeaveRoomPacket() { ID = (ushort)PacketId.LeaveRoom; }
}

[MessagePackObject]
public class LeaveRoomResponsePacket : PacketBase
{
    [Key(1)] public bool Success { get; set; }
    [Key(2)] public string Message { get; set; } = "";

    public LeaveRoomResponsePacket() { ID = (ushort)PacketId.LeaveRoomResponse; }
}

[MessagePackObject]
public class RoomInfoPacket : PacketBase
{
    [Key(1)] public List<UserElement> Players { get; set; } = new List<UserElement>();
    [Key(2)] public List<UserElement> Spectators { get; set; } = new List<UserElement>();

    public RoomInfoPacket() { ID = (ushort)PacketId.RoomInfo; }
}

[MessagePackObject]
public class UserElement
{
    [Key(0)] public string Nickname { get; set; } = "";
}

[MessagePackObject]
public class SwitchRolePacket : PacketBase
{
    public SwitchRolePacket() { ID = (ushort)PacketId.SwitchRole; }
}

[MessagePackObject]
public class SwitchRoleResponsePacket : PacketBase
{
    [Key(1)] public bool Success { get; set; }
    [Key(2)] public bool ToSpectator { get; set; }
    [Key(3)] public string Message { get; set; } = "";

    public SwitchRoleResponsePacket() { ID = (ushort)PacketId.SwitchRoleResponse; }
}
