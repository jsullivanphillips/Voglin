using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipes : MonoBehaviour
{
    public static CraftingRecipes Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<CraftingRecipeSO> craftingRecipes = new List<CraftingRecipeSO>();

    public bool DoesRecipeExist(ItemSO item1, ItemSO item2)
    {
        foreach(CraftingRecipeSO recipe in craftingRecipes)
        {
            if(recipe.item1.itemName == item1.itemName 
            && recipe.item2.itemName == item2.itemName)
            {
                return true;
            }
            else if(recipe.item1.itemName == item2.itemName 
            && recipe.item2.itemName == item1.itemName)
            {
                return true;
            }
        }
        return false;
    }

    public ItemSO GetCraftingResult(ItemSO item1, ItemSO item2)
    {
        foreach(CraftingRecipeSO recipe in craftingRecipes)
        {
            if(recipe.item1.itemName == item1.itemName 
            && recipe.item2.itemName == item2.itemName)
            {
                return recipe.result;
            }
            else if(recipe.item1.itemName == item2.itemName 
            && recipe.item2.itemName == item1.itemName)
            {
                return recipe.result;
            }
        }
        return null;
    }
}
