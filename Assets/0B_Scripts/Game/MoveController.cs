using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;

    private bool _isPressed;

    private void Awake()
    {
        InputManager.Instance.AddListener(InputType.Move, HandleMove);
        InputManager.Instance.AddListener(InputType.MoveCancel, HandleMoveCancel);
    }

    private void Update()
    {
        if(_isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, _whatIsGround))
            {
                MovePacket packet = new MovePacket
                {
                    Token = NetworkManager.Instance.Token,
                    X = hit.point.x,
                    Y = hit.point.z
                };

                NetworkManager.Instance.SendAsync(packet);
            }
        }
    }

    private void HandleMove() => _isPressed = true;
    private void HandleMoveCancel() => _isPressed = false;
}
