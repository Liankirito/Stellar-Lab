using UnityEngine;

[System.Serializable]
public struct LootItem
{
    public string resourceName; 
    public float dropChance;  
}

public class planet : MonoBehaviour
{
    public LootItem[] lootTable;

    void OnMouseDown()
    {
        DropResource();
    }

    void DropResource()
    {
        float totalChance = 0f;
        foreach (LootItem item in lootTable)
        {
            totalChance += item.dropChance;
        }

        float randomValue = Random.Range(0, totalChance);

        foreach (LootItem item in lootTable)
        {
            if (randomValue < item.dropChance)
            {
                InventoryManager.AddResource(item.resourceName, 1); 
                return;
            }
            else
            {
                randomValue -= item.dropChance;
            }
        }
    }
}