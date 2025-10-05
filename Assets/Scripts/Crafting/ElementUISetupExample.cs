using UnityEngine;
using UnityEngine.UI;

/**
 * 工作台元素 UI 設置範例
 * 這個腳本展示了如何為每個元素設置數量顯示
 */
public class ElementUISetupExample : MonoBehaviour
{
    [Header("Setup Instructions")]
    [TextArea(10, 20)]
    public string instructions = @"為了在工作台櫃子上顯示素材數量，請按照以下步驟設置：

1. 確保每個元素 GameObject 都有：
   - BaseElement 的子類別（如 O2, N2, H2 等）
   - Image 元件（用於顯示元素圖示）
   - Button 元件（用於點擊互動）

2. 為每個元素新增數量顯示：
   - 新增一個子物件作為數量顯示
   - 在子物件上添加 Text 元件
   - 將此 Text 元件設置到 BaseElement 的 inventoryCountText 欄位

3. 場景中新增 ElementInventoryDisplayManager：
   - 創建一個空的 GameObject
   - 添加 ElementInventoryDisplayManager 腳本
   - 設置適當的更新間隔（建議 0.5 秒）

4. 數量顯示的設置建議：
   - 文字顏色：有素材時為白色，無素材時為灰色
   - 文字位置：建議放在元素圖示的右下角
   - 文字大小：建議使用小字體，不要遮擋主要圖示

5. 視覺回饋設置：
   - changeColorWhenEmpty = true（當素材為空時改變顏色）
   - emptyColor = 淺灰色（無素材時的顏色）
   - availableColor = 正常顏色（有素材時的顏色）

現在當玩家收集到素材時，工作台上的對應元素會顯示數量，
當素材用完時，元素會變灰色並顯示 0。";

    void Start()
    {
        // 這只是一個說明腳本，實際上不需要執行任何邏輯
        Debug.Log("ElementUISetupExample loaded. Check the instructions in the inspector.");
    }
}