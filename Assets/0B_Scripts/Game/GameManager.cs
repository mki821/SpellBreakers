using System.Collections.Generic;
using UnityEngine;

[MonoSingletonUsage(MonoSingletonFlags.None)]
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private SerializableDictionary<EntityType, Entity> _entityPrefabs;

    private Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();

    public Entity Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PacketHandler.Register(PacketId.EntityInfo, HandleEntityInfo);
    }

    private void HandleEntityInfo(PacketBase packet)
    {
        EntityInfoPacket info = (EntityInfoPacket)packet;

        foreach (EntityInfo entityInfo in info.Entities)
        {
            if (!_entities.TryGetValue(entityInfo.EntityID, out Entity entity))
            {
                entity = CreateEntity(entityInfo);
                _entities.Add(entityInfo.EntityID, entity);
            }

            if(entityInfo.IsDead)
            {
                _entities.Remove(entityInfo.EntityID);
                Destroy(entity.gameObject);

                return;
            }

            entity.transform.position = entityInfo.Position.GetVector3();

            if (entity is Character character)
            {
                character.GetCharacterComponent<CharacterAnimator>().SetMoving(entityInfo.IsMoving);
            }
        }
    }
    
    private Entity CreateEntity(EntityInfo entityInfo)
    {
        Entity entity = Instantiate(GetEntityPrefab((EntityType)entityInfo.EntityType));

        if(entityInfo.EntityType == (ushort)EntityType.Character && entityInfo.EntityID == NetworkManager.Instance.Token)
        {
            Player = entity;
            _cameraController.SetTarget(Player.transform);
        }

        return entity;
    }

    private Entity GetEntityPrefab(EntityType type)
    {
        _entityPrefabs.TryGetValue(type, out Entity entity);
        return entity;
    }
}
