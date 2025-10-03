using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

[Serializable]

public struct DialoguePiece
{
    public string name;
    [TextArea] public string dialogue;

}

public class dialogueScript : MonoBehaviour
{
    //Dialogue Variables
    public List<DialoguePiece> dialogue;
    
}
