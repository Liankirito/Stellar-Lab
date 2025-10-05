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
}