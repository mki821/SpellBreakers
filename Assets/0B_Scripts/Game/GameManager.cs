using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[MonoSingletonUsage(MonoSingletonFlags.None)]
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private SerializableDictionary<EntityType, Entity> _entityPrefabs;

    private readonly Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();
    private readonly HashSet<string> _receivedIds = new HashSet<string>();

    public Entity Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PacketHandler.Register(PacketId.EntityInfo, HandleEntityInfo);
    }

    private void HandleEntityInfo(PacketBase packet)
    {
        EntityInfoPacket info = (EntityInfoPacket)packet;

        if (info.Tick < NetworkManager.Instance.LastTick) return;
        NetworkManager.Instance.LastTick = info.Tick;

        _receivedIds.Clear();

        foreach (EntityInfo entityInfo in info.Entities)
        {
            _receivedIds.Add(entityInfo.EntityID);

            if (!_entities.TryGetValue(entityInfo.EntityID, out Entity entity))
            {
                entity = CreateEntity(entityInfo);
                _entities.Add(entityInfo.EntityID, entity);
            }

            entity.transform.position = entityInfo.Position.GetVector3();

            if (entity is Character character)
            {
                character.GetCharacterComponent<CharacterAnimator>().SetMoving(entityInfo.IsMoving);
            }
        }

        List<string> destroyIds = new List<string>();
        foreach (string id in _entities.Keys)
        {
            if (!_receivedIds.Contains(id))
            {
                destroyIds.Add(id);
            }
        }
        
        for (int i = 0; i < destroyIds.Count; ++i)
        {
            Destroy(_entities[destroyIds[i]].gameObject);
            _entities.Remove(destroyIds[i]);
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
