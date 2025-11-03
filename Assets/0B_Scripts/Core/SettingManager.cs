using UnityEngine;

public enum ResolutionType
{
    QHD, FHD, HD
}

[MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
public class SettingManager : MonoSingleton<SettingManager>
{
    protected override void Awake()
    {
        base.Awake();

        int height = Screen.height;
        Vector2Int resolution = height switch
        {
            >= 1440 => new Vector2Int(2560, 1440),
            >= 1080 => new Vector2Int(1920, 1080),
            _ => new Vector2Int(1200, 700),
        };

        Screen.SetResolution(resolution.x, resolution.y, true);
    }

    public void SetResolution(ResolutionType type)
    {
        Vector2Int resolution = type switch
        {
            ResolutionType.QHD => new Vector2Int(2560, 1440),
            ResolutionType.FHD => new Vector2Int(1920, 1080),
            _ => new Vector2Int(1200, 700),
        };

        Screen.SetResolution(resolution.x, resolution.y, true);
    }
}
