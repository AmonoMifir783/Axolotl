using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsBase", menuName = "Scriptable Objects/ItemsBase")]
public class ItemsBase : ScriptableObject
{
    public List<ItemInform> Items = new List<ItemInform>();
}

[Serializable]
public class ItemInform
{
    public ItemTag tag;
    public GameObject prefabItem;
    
    public bool PickUp;
    public bool Transform;
    public bool Break;
    
    public ParentClass[] parentClasses;
    public ItemTag[] DropedTag;
    
    public float hp;
    public float st;
    public float hu;
}

public enum ItemTag
{
    Seed,
    Algae,
    Stone,
    Flint,
    BigAlgae,
    Coral,
    Alga_square,
    Destructible_Stone
}

public enum ParentClass
{
    Edible,
    E
}
