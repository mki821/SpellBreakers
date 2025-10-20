using System.Reflection;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    private static MonoSingletonFlags? _flags;
    private static object _locker = new object();

    public static T Instance
    {
        get
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        _instance = new GameObject(nameof(T)).AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
    }
    
    protected virtual void Awake()
    {
        if(!_flags.HasValue)
        {
            MonoSingletonUsageAttribute attribute = typeof(T).GetCustomAttribute<MonoSingletonUsageAttribute>();
            _flags = attribute != null ? attribute.Flag : MonoSingletonFlags.None;

            if(_flags == MonoSingletonFlags.DontDestroyOnLoad)
            {
                if (Instance == this)
                {
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
