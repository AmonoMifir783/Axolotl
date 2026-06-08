using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CraftBase", menuName = "Scriptable Objects/CraftBase")]
public class CraftBase : ScriptableObject
{
    public List<CraftInform> Crafts = new List<CraftInform>();
}

[Serializable]
public class CraftInform
{
    public ItemTag tag_1;
    public ItemTag tag_2;
    public ItemTag tag_result;
}

// public enum ItemFirstTag
// {
//     Seed,
//     Algae,
//     Stone,
//     Flint,
//     BigAlgae,
//     Coral,
//     Alga_square,
//     Destructible_Stone
// }
