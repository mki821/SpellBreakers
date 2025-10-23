using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomUI : MonoBehaviour
{
    [System.Serializable]
    private struct UserInfo
    {
        public Image background;
        public TextMeshProUGUI nicknameText;
    }

    [SerializeField] private List<UserInfo> _userInfos;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _deactiveColor;

    [SerializeField] private GameObject _temp;

    private void Awake()
    {
        PacketHandler.Register(PacketId.RoomInfo, UpdateRoomInfo);
        PacketHandler.Register(PacketId.SwitchRoleResponse, (packet) => { });
        PacketHandler.Register(PacketId.LeaveRoomResponse, HandleLeaveRoom);
    }

    private void UpdateRoomInfo(PacketBase packet)
    {
        RoomInfoPacket roomInfo = (RoomInfoPacket)packet;

        int playersCount = roomInfo.Players.Count;
        int spectatorsCount = roomInfo.Spectators.Count;

        for (int i = 0; i < playersCount; ++i)
        {
            _userInfos[i].background.color = _activeColor;
            _userInfos[i].nicknameText.text = roomInfo.Players[i].Nickname;
        }

        for (int i = playersCount; i < 2; ++i)
        {
            _userInfos[i].background.color = _deactiveColor;
            _userInfos[i].nicknameText.text = string.Empty;
        }

        for (int i = 2; i < spectatorsCount + 2; ++i)
        {
            _userInfos[i].background.color = _activeColor;
            _userInfos[i].nicknameText.text = roomInfo.Spectators[i - 2].Nickname;
        }

        for (int i = spectatorsCount + 2; i < 6; ++i)
        {
            _userInfos[i].background.color = _deactiveColor;
            _userInfos[i].nicknameText.text = string.Empty;
        }
    }
    
    public void SwitchRole()
    {
        SwitchRolePacket packet = new SwitchRolePacket();
        NetworkManager.Instance.SendAsync(packet);
    }

    public void Leave()
    {
        LeaveRoomPacket packet = new LeaveRoomPacket();
        NetworkManager.Instance.SendAsync(packet);
    }
    
    private void HandleLeaveRoom(PacketBase packet)
    {
        LeaveRoomResponsePacket response = (LeaveRoomResponsePacket)packet;

        if (response.Success)
        {
            _temp.SetActive(true);
        }
        else
        {
            Debug.Log(response.Message);
        }
    }
}
