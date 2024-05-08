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

    public bool DoesRecipeExist(ComponentSO item1, ComponentSO item2)
    {
        foreach(CraftingRecipeSO recipe in craftingRecipes)
        {
            if(recipe.item1.name == item1.name 
            && recipe.item2.name == item2.name)
            {
                return true;
            }
            else if(recipe.item1.name == item2.name 
            && recipe.item2.name == item1.name)
            {
                return true;
            }
        }
        return false;
    }

    public ComponentSO GetCraftingResult(ComponentSO item1, ComponentSO item2)
    {
        foreach(CraftingRecipeSO recipe in craftingRecipes)
        {
            if(recipe.item1.name == item1.name 
            && recipe.item2.name == item2.name)
            {
                return Instantiate(recipe.result);
            }
            else if(recipe.item1.name == item2.name 
            && recipe.item2.name == item1.name)
            {
                return Instantiate(recipe.result);
            }
        }
        return null;
    }
}
