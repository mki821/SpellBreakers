using System.Collections.Generic;
using UnityEngine;

[MonoSingletonUsage(MonoSingletonFlags.None)]
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private SerializableDictionary<EntityType, Entity> _entityPrefabs;

    private Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();

    protected override void Awake()
    {
        base.Awake();

        PacketHandler.Register(PacketId.EntityInfo, HandleEntityInfo);
    }

    private void HandleEntityInfo(PacketBase packet)
    {
        EntityInfoPacket info = (EntityInfoPacket)packet;

        for(int i = 0; i < info.Entities.Count; ++i)
        {
            EntityInfo entityInfo = info.Entities[i];

            if (!_entities.TryGetValue(entityInfo.EntityID, out Entity entity))
            {
                entity = Instantiate(GetEntityPrefab((EntityType)entityInfo.EntityType));
                _entities.Add(entityInfo.EntityID, entity);
            }

            entity.transform.position = new Vector3(entityInfo.X, 0.0f, entityInfo.Y);
        }
    }

    private Entity GetEntityPrefab(EntityType type)
    {
        _entityPrefabs.TryGetValue(type, out Entity entity);
        return entity;
    }
}
