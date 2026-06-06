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


// родительский класс
    public bool PickUp;
    public bool Transform;
    public bool Break;
    //--------------------
    public ParentClass[] parentClasses;
//-------------------

    public ItemTag[] DropedTag;
}

public enum ItemTag
{
    Seed,
    Algae,
    Stone,
    Flint,
    BigAlgae,
    Coral,
    Alga_square
}

public enum ParentClass
{
    R,
    E
}
