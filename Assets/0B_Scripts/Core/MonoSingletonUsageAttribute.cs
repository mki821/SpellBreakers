using System;

[Flags]
public enum MonoSingletonFlags : ushort
{
    None = 0, DontDestroyOnLoad = 1 << 0
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MonoSingletonUsageAttribute : Attribute
{
    public MonoSingletonFlags Flag;

    public MonoSingletonUsageAttribute(MonoSingletonFlags flag) => Flag = flag;
}
