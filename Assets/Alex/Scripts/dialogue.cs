using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dialogue", menuName = "Scriptable Objects/dialogue")]
public class dialogue : ScriptableObject
{
    public List<DialoguePiece> lines;
}
