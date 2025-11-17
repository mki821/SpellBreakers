using System.Collections.Generic;
using UnityEngine;

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

        UIManager.Instance.GetUI(UIType.Room).gameObject.SetActive(false);

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
            string id = entityInfo.EntityID;

            _receivedIds.Add(id);

            if (!_entities.TryGetValue(id, out Entity entity))
            {
                entity = CreateEntity(entityInfo);
                _entities.Add(id, entity);

                if(entity is Character)
                {
                    UIManager.Instance.HUDUI.AddHUD(id, HUDType.Status);
                }
            }

            entity.transform.position = entityInfo.Position.GetVector3();

            if (entity is Character character)
            {
                character.SetInfo((CharacterInfo)entityInfo);
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
            if (_entities[destroyIds[i]] is Character)
            {
                UIManager.Instance.HUDUI.RemoveHUD(destroyIds[i]);
            }
            
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
