using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private RoomElementUI _prefab;
    [SerializeField] private float _offset;

    private void Awake()
    {
        PacketHandler.Register(PacketId.ListRoomResponse, HandleListRoom);
        PacketHandler.Register(PacketId.JoinRoomResponse, HandleJoinRoom);
    }

    private void HandleListRoom(PacketBase packet)
    {
        ListRoomResponsePacket list = (ListRoomResponsePacket)packet;

        _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, _offset * list.Rooms.Count);

        for (int i = 0; i < list.Rooms.Count; ++i)
        {
            RoomElementUI element = Instantiate(_prefab, _contentTransform);
            element.Initialize(list.Rooms[i]);
        }
    }

    private void HandleJoinRoom(PacketBase packet)
    {
        gameObject.SetActive(false);
    }

    [ContextMenu("CreateRoom")]
    private void CreateRoom()
    {
        CreateRoomPacket packet = new CreateRoomPacket();
        packet.Name = "TestRoom01";
        packet.Password = "";

        NetworkManager.Instance.SendAsync(packet);
    }

    [ContextMenu("ListRoom")]
    private void ListRoom()
    {
        ListRoomPacket packet = new ListRoomPacket();

        NetworkManager.Instance.SendAsync(packet);
    }
}
