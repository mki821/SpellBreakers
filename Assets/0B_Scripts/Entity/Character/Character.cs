using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : Entity
{
    [SerializeField] private float _statusUIOffset;

    private readonly Dictionary<Type, ICharacterComponent> _components = new Dictionary<Type, ICharacterComponent>();

    private Vector3 _previousPosition;

    private void Awake()
    {
        List<ICharacterComponent> components = GetComponentsInChildren<ICharacterComponent>().ToList();
        components.ForEach(component => component.Initialize(this));
        components.ForEach(component => _components.Add(component.GetType(), component));
        
        _previousPosition = transform.position;
    }

    public T GetCharacterComponent<T>() where T : class, ICharacterComponent
    {
        _components.TryGetValue(typeof(T), out ICharacterComponent component);
        return component as T;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (_previousPosition == currentPosition) return;

        Vector3 direction = (currentPosition - _previousPosition).normalized;
        direction.y = 0.0f;

        transform.rotation = Quaternion.LookRotation(direction);

        _previousPosition = currentPosition;
    }
    
    public void SetInfo(CharacterInfo info)
    {
        StatusUI status = UIManager.Instance.HUDUI.GetUI<StatusUI>(info.EntityID);
        status.transform.position = Camera.main.WorldToScreenPoint(info.Position.GetVector3()) + Vector3.up * _statusUIOffset;
        status.SetHealth(info.CurrentHealth / info.Stat.MaxHealth);
    }
}
