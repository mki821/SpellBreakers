using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessagePack;

public static class UdpPacketHelper
{
    public static async Task SendAsync<T>(Socket socket, EndPoint endPoint, T packet) where T : UdpPacketBase
    {
        byte[] body = MessagePackSerializer.Serialize(packet);
        await socket.SendToAsync(body, SocketFlags.None, endPoint);
    }

    public static PacketBase Deserialize(byte[] data, int length)
    {
        PacketBase temp = MessagePackSerializer.Deserialize<PacketBase>(data.AsMemory(0, length));
        Type type = PacketRegistry.GetTypeById(temp.ID);

        return (PacketBase)MessagePackSerializer.Deserialize(type, data.AsMemory(0, length));
    }
}
