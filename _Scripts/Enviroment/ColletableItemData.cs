using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item_", menuName ="ScriptableObject/CollectableItem")]
public class ColletableItemData : ScriptableObject
{
    public Sprite sprite;
    public CollectableItemType type;
}

public enum CollectableItemType {
    Cannon,
    Health,
    Survior
}