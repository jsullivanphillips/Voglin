using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Recipes/New Recipe")]
public class CraftingRecipeSO : ScriptableObject
{
    public ItemSO item1;
    public ItemSO item2;
    public ItemSO result;
}