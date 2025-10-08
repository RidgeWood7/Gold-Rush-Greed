using UnityEngine;

[CreateAssetMenu(fileName = "CollectionData", menuName = "Scriptable Objects/CollectionData")]
public class CollectionData : ScriptableObject
{
    public enum TypeEnum
    {
        Dust,
        Ingot
    }
    public TypeEnum type;
    public int weightAdding;
}
