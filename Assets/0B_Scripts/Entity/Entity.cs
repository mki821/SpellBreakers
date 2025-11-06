using UnityEngine;

public class Entity : MonoBehaviour
{
    public string ID { get; set; }

    public void SetID(string id)
    {
        ID = id;
    }
}
