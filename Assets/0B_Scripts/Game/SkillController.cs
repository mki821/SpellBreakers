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

        Vector3 position = GameManager.Instance.Player.transform.position;

        packet.SpawnPosition = new Vector(position.x, 0.5f, position.z);
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, _whatIsGround))
        {
            packet.TargetPosition = new Vector(hit.point);

            NetworkManager.Instance.SendAsync(packet);
        }
    }
}
