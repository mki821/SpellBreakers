using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using MessagePack;
using MessagePack.Resolvers;

[MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
public class NetworkManager : MonoSingleton<NetworkManager>
{
    [SerializeField] private string _ip;
    [SerializeField] private int _port;

    [SerializeField, ReadOnly] private string _token;

    private Socket _tcpSocket;
    private Socket _udpSocket;

    private Queue<Action> _commandQueue = new Queue<Action>();

    protected override void Awake()
    {
        base.Awake();

        StaticCompositeResolver.Instance.Register(
            GeneratedResolver.Instance,
            StandardResolver.Instance
        );

        var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
        MessagePackSerializer.DefaultOptions = option;

        PacketHandler.Register(PacketId.LoginResponse, (packet) => _token = ((LoginResponsePacket)packet).IssuedToken);

        _ = Task.Run(() => HandleTcpListen());
        _ = Task.Run(() => HandleUdpListen());
    }

    private void Update()
    {
        while(_commandQueue.Count > 0)
        {
            _commandQueue.Dequeue()?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _tcpSocket.Close();
        _udpSocket.Close();
    }

    private void OnApplicationQuit()
    {
        _tcpSocket.Close();
        _udpSocket.Close();
    }

    private async Task HandleTcpListen()
    {
        _tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        await _tcpSocket.ConnectAsync(_ip, _port);

        Debug.Log("[클라이언트] Tcp 서버 연결됨!");

        try
        {
            while (true)
            {
                PacketBase packet = await TcpPacketHelper.ReceiveAsync(_tcpSocket);
                if (packet == null) continue;

                Action handler = PacketHandler.Handle((PacketId)packet.ID, packet);
                if(handler != null)
                {
                    _commandQueue.Enqueue(handler);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"[클라이언트] 서버 오류 : {ex.Message}");
        }
        finally
        {
            Debug.Log("[클라이언트] 연결 끊김.");

            _tcpSocket.Close();
        }
    }

    private async Task HandleUdpListen()
    {
        _udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        await _udpSocket.ConnectAsync(_ip, _port + 1);

        byte[] buffer = new byte[1024];

        while (true)
        {
            SocketReceiveFromResult result = await _udpSocket.ReceiveFromAsync(buffer, SocketFlags.None, new IPEndPoint(IPAddress.Any, 0));

            PacketBase packet = UdpPacketHelper.Deserialize(buffer, result.ReceivedBytes);
            if (packet == null) return;

            if (packet is UdpPacketBase)
            {
                Action handler = PacketHandler.Handle((PacketId)packet.ID, packet);
                if(handler != null)
                {
                    _commandQueue.Enqueue(handler);
                }
            }
        }
    }
    
    public async void SendAsync<T>(T packet) where T : PacketBase
    {
        await TcpPacketHelper.SendAsync(_tcpSocket, packet);
    }
}
