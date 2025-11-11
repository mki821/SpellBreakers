using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;

    private void Awake()
    {
        InputManager.Instance.AddListener(InputType.Skill1, FireProjectile);
    }

    private void FireProjectile()
    {
        FireProjectilePacket packet = new FireProjectilePacket();

        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 80.0f, _whatIsGround))
        {
            Vector3 position = GameManager.Instance.Player.transform.position;

            packet.OwnerID = NetworkManager.Instance.Token;
            packet.SpawnPosition = new Vector(position.x, 0.5f, position.z);
            packet.TargetPosition = new Vector(hit.point);

            NetworkManager.Instance.SendAsync(packet);
        }
    }
}
