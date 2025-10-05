using UnityEngine;

// [System.Serializable] 讓我們可以在 Inspector 視窗中看到並編輯這個結構
[System.Serializable]
public struct LootItem
{
    public string resourceName;  // 資源名稱
    public float dropChance;     // 掉落機率 (權重)
}

public class planet : MonoBehaviour
{
    // 在 Inspector 視窗中設定這個星球的掉落物清單
    public LootItem[] lootTable;

    void OnMouseDown()
    {
        // 呼叫我們的掉寶函式
        DropResource();
    }

    // 在 PlanetLoot.cs 裡面

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
                // 我們抽中這個了！
                // 把原本的 Debug.Log(...) 換成呼叫我們的倉庫管理員
                InventoryManager.AddResource(item.resourceName, 1); // 假設一次只掉落 1 個
                return;
            }
            else
            {
                randomValue -= item.dropChance;
            }
        }
    }
}