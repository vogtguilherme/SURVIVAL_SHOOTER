using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;

    [Multiline(5)]
    public string description;

    public int cost;
    public bool bought;
    public Sprite image;
}
