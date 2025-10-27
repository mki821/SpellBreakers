using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool _flag;
    [SerializeField] private float _speed;

    private void Awake()
    {
        if(!_flag)
        {
            PacketHandler.Register(PacketId.Move, HandleMove);
        }
    }

    private void HandleMove(PacketBase packet)
    {
        MovePacket move = (MovePacket)packet;

        if(move.Token != NetworkManager.Instance.Token)
        {
            transform.position = new Vector2(move.X, move.Y);
        }
    }

    private void Update()
    {
        Vector2 movement = InputManager.Instance.GetMovement();

        if (!_flag || movement.sqrMagnitude == 0) return;

        transform.position += (Vector3)movement.normalized * (_speed * Time.deltaTime);

        MovePacket packet = new MovePacket
        {
            Token = NetworkManager.Instance.Token,
            X = transform.position.x,
            Y = transform.position.y
        };

        NetworkManager.Instance.SendUdpAsync(packet);
    }
}
