using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager
{
    public static Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static void AddResource(string resourceName, int amount)
    {
        if (inventory.ContainsKey(resourceName))
        {
            inventory[resourceName] += amount;
        }
        else
        {
            inventory.Add(resourceName, amount);
        }

        Debug.Log("Obtain: " + resourceName + " x" + amount + "。 total: " + inventory[resourceName]);
        
        NotifyInventoryChanged();
    }

    public static int GetResourceCount(string resourceName)
    {
        if (inventory.ContainsKey(resourceName))
        {
            return inventory[resourceName];
        }
        else
        {
            return 0;
        }
    }
    
    public static bool UseResource(string resourceName, int amount)
    {
        if (GetResourceCount(resourceName) >= amount)
        {
            inventory[resourceName] -= amount;
            
            if (inventory[resourceName] <= 0)
            {
                inventory[resourceName] = 0;
            }
            
            Debug.Log($"Used: {resourceName} x{amount}. Remaining: {inventory[resourceName]}");
            
            NotifyInventoryChanged();
            
            return true;
        }
        else
        {
            Debug.LogWarning($"Not enough {resourceName}! Available: {GetResourceCount(resourceName)}, Required: {amount}");
            return false;
        }
    }
    
    private static void NotifyInventoryChanged()
    {
        ElementInventoryDisplayManager displayManager = Object.FindObjectOfType<ElementInventoryDisplayManager>();
        if (displayManager != null)
        {
            displayManager.ForceUpdateAllElementsDisplay();
        }
    }
}