using UnityEngine;

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
        PacketHandler.Register(PacketId.LeaveRoomResponse, _roomInfo.HandleLeaveRoom);
        
        PacketHandler.Register(PacketId.Chat, _roomInfo.Chat.UpdateChat);
        
        ListRoomPacket list = new ListRoomPacket();
        NetworkManager.Instance.SendAsync(list);
    }
}
