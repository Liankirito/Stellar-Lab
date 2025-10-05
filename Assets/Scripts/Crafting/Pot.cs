using UnityEngine;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    [Header("Required Components")]
    public ElementManager elementManager;
    public CraftingFormula craftingFormula;
    
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public bool destroyOnCraft = false;
    
    [Header("Feedback")]
    public bool showCraftingFeedback = true;
    
    public GameObject TryCraft()
    {
        if (elementManager == null)
        {
            Debug.LogError("ElementManager is not assigned!");
            return null;
        }
        
        if (craftingFormula == null)
        {
            Debug.LogError("CraftingFormula is not assigned!");
            return null;
        }
        
        List<string> currentElements = elementManager.GetAddedElements();
        
        if (currentElements.Count == 0)
        {
            if (showCraftingFeedback)
                Debug.Log("No elements in the pot!");
            return null;
        }
        
        Recipe matchingRecipe = craftingFormula.GetRecipeByElements(currentElements);
        
        if (matchingRecipe != null)
        {
            if (showCraftingFeedback)
                Debug.Log($"Successfully crafted: {matchingRecipe.planetName}!");
            
            GameObject planet = CreatePlanet(matchingRecipe);
            
            if (destroyOnCraft)
            {
                elementManager.ClearAllElements();
            }
            
            return planet;
        }
        else
        {
            if (showCraftingFeedback)
            {
                string elementsList = string.Join(", ", currentElements);
                Debug.Log($"No recipe found for elements: {elementsList}");
            }
            return null;
        }
    }
    
    private GameObject CreatePlanet(Recipe recipe)
    {
        if (recipe.planetPrefab == null)
        {
            Debug.LogWarning($"Planet prefab for {recipe.planetName} is not assigned!");
            return null;
        }
        
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;
        
        GameObject planet = Instantiate(recipe.planetPrefab, position, rotation);
        
        return planet;
    }
    
    public void AddElementToPot(string elementName)
    {
        if (elementManager != null)
        {
            elementManager.AddElement(elementName);
        }
    }
    
    public List<string> GetCurrentElements()
    {
        return elementManager != null ? elementManager.GetAddedElements() : new List<string>();
    }
    
    public void ClearPot()
    {
        if (elementManager != null)
        {
            elementManager.ClearAllElements();
        }
    }
}
