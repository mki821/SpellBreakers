using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomInfoUI : UIBase
{
    [SerializeField] private List<RoomInfoUserElementUI> _userElements;
    [SerializeField] private TextMeshProUGUI _readyText;

    [field: SerializeField] public ChatUI Chat { get; private set; }

    public void UpdateRoomInfo(PacketBase packet)
    {
        RoomInfoPacket roomInfo = (RoomInfoPacket)packet;

        UpdateUserElements(roomInfo.Players, 0, 2);
        UpdateUserElements(roomInfo.Spectators, 2, 6);
    }

    private void UpdateUserElements(List<UserElement> elements, int start, int end)
    {
        for (int i = 0; i < elements.Count && start < end; ++i, ++start)
        {
            _userElements[start].SetInfo(elements[i]);
        }

        for (; start < end; ++start)
        {
            _userElements[start].SetInfo(null);
        }
    }

    public void HandleSwitchRole(PacketBase packet)
    {
        SwitchRoleResponsePacket response = (SwitchRoleResponsePacket)packet;

        if (!response.Success)
        {
            UIManager.Instance.PopupUI.AddPopup<WarningPopupUI>(PopupType.Warning).SetText(response.Message);
        }
    }
    
    public void HandleReady(PacketBase packet)
    {
        ReadyResponsePacket response = (ReadyResponsePacket)packet;

        _readyText.text = response.IsReady ? "준비 취소" : "준비 완료";
    }

    public void HandleLeaveRoom(PacketBase packet)
    {
        LeaveRoomResponsePacket response = (LeaveRoomResponsePacket)packet;

        if (response.Success)
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIType.RoomList).gameObject.SetActive(true);
        }
        else
        {
            UIManager.Instance.PopupUI.AddPopup<WarningPopupUI>(PopupType.Warning).SetText(response.Message);
        }
    }

    public void SwitchRole()
    {
        SwitchRolePacket packet = new SwitchRolePacket();
        NetworkManager.Instance.SendAsync(packet);
    }
    
    public void Ready()
    {
        ReadyPacket packet = new ReadyPacket();
        NetworkManager.Instance.SendAsync(packet);
    }

    public void Leave()
    {
        LeaveRoomPacket packet = new LeaveRoomPacket();
        NetworkManager.Instance.SendAsync(packet);
    }
}
