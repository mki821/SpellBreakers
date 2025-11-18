using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;

    private void Awake()
    {
        InputManager.Instance.AddListener(InputType.Skill1, Skill1);
    }

    private void Skill1()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 80.0f, _whatIsGround))
        {
            Vector3 position = GameManager.Instance.Player.transform.position;

            SkillPacket packet = new SkillPacket
            {
                SkillType = 1,
                OwnerID = NetworkManager.Instance.Token,
                SpawnPosition = new Vector(position.x, 0.5f, position.z),
                TargetPosition = new Vector(hit.point)
            };

            NetworkManager.Instance.SendAsync(packet);
        }
    }
}
