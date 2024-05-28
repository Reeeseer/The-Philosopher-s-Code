//This script is used for holding data about the ingredients the player has access to


using System;
using UnityEngine;

[Serializable]
public class Ingredient : ScriptableObject
{
    public string Name;
    public int Amount;
    public int StartingAmount;
    [TextArea] public string Description;

    public void Restart()
    {
        Amount = StartingAmount;
    }
}