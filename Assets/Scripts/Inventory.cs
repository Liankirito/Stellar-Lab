using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager
{
    // 我們使用一個「字典」(Dictionary) 來儲存背包內容
    // Key 是資源名稱 (string), Value 是資源數量 (int)
    public static Dictionary<string, int> inventory = new Dictionary<string, int>();

    // 加入資源到背包的方法
    public static void AddResource(string resourceName, int amount)
    {
        // 如果背包裡已經有這個資源了
        if (inventory.ContainsKey(resourceName))
        {
            // 數量直接加上去
            inventory[resourceName] += amount;
        }
        else
        {
            // 如果是新資源，就新增一個項目
            inventory.Add(resourceName, amount);
        }

        Debug.Log("Obtain: " + resourceName + " x" + amount + "。 total: " + inventory[resourceName]);
        
        // 通知元素顯示管理器更新顯示
        NotifyInventoryChanged();
    }

    // 查詢某個資源數量的方法 (未來會用到)
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
    
    /// <summary>
    /// 使用（消耗）資源
    /// </summary>
    /// <param name="resourceName">資源名稱</param>
    /// <param name="amount">使用數量</param>
    /// <returns>是否成功使用</returns>
    public static bool UseResource(string resourceName, int amount)
    {
        if (GetResourceCount(resourceName) >= amount)
        {
            inventory[resourceName] -= amount;
            
            // 如果數量變為 0，可以選擇是否移除該項目（這裡保留為 0）
            if (inventory[resourceName] <= 0)
            {
                inventory[resourceName] = 0;
            }
            
            Debug.Log($"Used: {resourceName} x{amount}. Remaining: {inventory[resourceName]}");
            
            // 通知元素顯示管理器更新顯示
            NotifyInventoryChanged();
            
            return true;
        }
        else
        {
            Debug.LogWarning($"Not enough {resourceName}! Available: {GetResourceCount(resourceName)}, Required: {amount}");
            return false;
        }
    }
    
    /// <summary>
    /// 通知所有元素更新數量顯示
    /// </summary>
    private static void NotifyInventoryChanged()
    {
        ElementInventoryDisplayManager displayManager = Object.FindObjectOfType<ElementInventoryDisplayManager>();
        if (displayManager != null)
        {
            displayManager.ForceUpdateAllElementsDisplay();
        }
    }
}