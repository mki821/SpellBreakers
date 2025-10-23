using UnityEngine;

public class RoomListUI : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private CreateRoomUI _createRoom;
    [SerializeField] private RoomElementUI _prefab;
    [SerializeField] private float _offset;

    private void Awake()
    {
        PacketHandler.Register(PacketId.ListRoomResponse, HandleListRoom);
        PacketHandler.Register(PacketId.JoinRoomResponse, HandleJoinRoom);
    }

    private void HandleListRoom(PacketBase packet)
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

    private void HandleJoinRoom(PacketBase packet)
    {
        JoinRoomResponsePacket response = (JoinRoomResponsePacket)packet;

        if (response.Success)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(response.Message);
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
