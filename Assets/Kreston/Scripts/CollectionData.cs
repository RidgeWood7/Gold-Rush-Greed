using UnityEngine;

[CreateAssetMenu(fileName = "CollectionData", menuName = "Scriptable Objects/CollectionData")]
public class CollectionData : ScriptableObject
{
    public enum TypeEnum
    {
        Dust,
        Ingot,
        Coal
    }
    public TypeEnum type;
}
