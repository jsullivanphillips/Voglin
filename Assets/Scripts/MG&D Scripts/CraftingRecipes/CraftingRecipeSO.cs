using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Recipes/New Recipe")]
public class CraftingRecipeSO : ScriptableObject
{
    public ComponentSO item1;
    public ComponentSO item2;
    public ComponentSO result;
}