using UnityEngine;

public class RoomListUI : UIBase
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private RoomElementUI _prefab;
    [SerializeField] private float _offset;
    
    [SerializeField] private CreateRoomUI _createRoom;

    public void HandleListRoom(PacketBase packet)
    {
        ListRoomResponsePacket response = (ListRoomResponsePacket)packet;

        foreach (Transform child in _contentTransform)
        {
            Destroy(child.gameObject);
        }

        _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, _offset * response.Rooms.Count);

        for (int i = 0; i < response.Rooms.Count; ++i)
        {
            RoomElementUI element = Instantiate(_prefab, _contentTransform);
            element.Initialize(response.Rooms[i]);
        }
    }

    public void HandleJoinRoom(PacketBase packet)
    {
        JoinRoomResponsePacket response = (JoinRoomResponsePacket)packet;

        if (response.Success)
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIType.RoomInfo).gameObject.SetActive(true);
            (UIManager.Instance.GetUI(UIType.RoomInfo) as RoomInfoUI).Chat.ClearChat();
        }
        else
        {
            UIManager.Instance.PopupUI.AddPopup<WarningPopupUI>(PopupType.Warning).SetText(response.Message);
        }
    }

    public void CreateRoom()
    {
        _createRoom.gameObject.SetActive(true);
    }

    public void ListRoom()
    {
        ListRoomPacket packet = new ListRoomPacket();

        NetworkManager.Instance.SendAsync(packet);
    }
}
