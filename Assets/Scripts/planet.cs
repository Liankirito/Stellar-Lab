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

    void DropResource()
    {
        float totalChance = 0f;
        // 計算總權重
        foreach (LootItem item in lootTable)
        {
            totalChance += item.dropChance;
        }

        // 產生一個 0 到總權重之間的隨機數
        float randomValue = Random.Range(0, totalChance);

        // 輪盤法決定掉落物
        foreach (LootItem item in lootTable)
        {
            if (randomValue < item.dropChance)
            {
                // 我們抽中這個了！
                Debug.Log(gameObject.name + " 掉落了資源: " + item.resourceName);
                // 找到資源後就可以跳出迴圈了
                return;
            }
            else
            {
                // 減去這個物品的權重，繼續抽下一個
                randomValue -= item.dropChance;
            }
        }
    }
}