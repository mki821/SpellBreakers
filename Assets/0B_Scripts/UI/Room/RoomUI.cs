using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomUI : UIBase
{
    [SerializeField] private RoomListUI _roomList;
    [SerializeField] private RoomInfoUI _roomInfo;

    private void Awake()
    {
        PacketHandler.Register(PacketId.ListRoomResponse, _roomList.HandleListRoom);
        PacketHandler.Register(PacketId.JoinRoomResponse, _roomList.HandleJoinRoom);

        PacketHandler.Register(PacketId.RoomInfo, _roomInfo.UpdateRoomInfo);
        PacketHandler.Register(PacketId.SwitchRoleResponse, _roomInfo.HandleSwitchRole);
        PacketHandler.Register(PacketId.ReadyResponse, _roomInfo.HandleReady);
        PacketHandler.Register(PacketId.LeaveRoomResponse, _roomInfo.HandleLeaveRoom);

        PacketHandler.Register(PacketId.Chat, _roomInfo.Chat.UpdateChat);

        PacketHandler.Register(PacketId.StartGame, StartGame);

        ListRoomPacket list = new ListRoomPacket();
        NetworkManager.Instance.SendAsync(list);
    }
    
    private void StartGame(PacketBase packet)
    {
        SceneManager.LoadScene(1);
    }
}
