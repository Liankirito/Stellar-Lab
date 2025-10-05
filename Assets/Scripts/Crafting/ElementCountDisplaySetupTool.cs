using UnityEngine;
using UnityEngine.UI;

/**
 * 自動設置工作台元素數量顯示的工具(AI-assisted)
 */
public class ElementCountDisplaySetupTool : MonoBehaviour
{
    [Header("Auto Setup Settings")]
    public Font textFont; // 要使用的字體
    public int fontSize = 14; // 字體大小
    public Color textColor = Color.white; // 文字顏色
    public Vector2 countTextOffset = new Vector2(15, -15); // 數量文字相對於元素的偏移
    
    [Header("Actions")]
    [Space(10)]
    public bool setupAllElements = false; // 在 Inspector 中勾選此項會自動設置所有元素
    
    void Update()
    {
        // 當在 Inspector 中勾選 setupAllElements 時執行設置
        if (setupAllElements)
        {
            setupAllElements = false;
            SetupAllElementsCountDisplay();
        }
    }
    
    /// <summary>
    /// 為場景中的所有元素設置數量顯示
    /// </summary>
    public void SetupAllElementsCountDisplay()
    {
        BaseElement[] elements = FindObjectsOfType<BaseElement>();
        int setupCount = 0;
        
        foreach (BaseElement element in elements)
        {
            if (SetupElementCountDisplay(element))
            {
                setupCount++;
            }
        }
        
        Debug.Log($"設置完成！為 {setupCount} 個元素添加了數量顯示。");
    }
    
    /// <summary>
    /// 為單個元素設置數量顯示
    /// </summary>
    /// <param name="element">要設置的元素</param>
    /// <returns>是否成功設置</returns>
    private bool SetupElementCountDisplay(BaseElement element)
    {
        if (element == null) return false;
        
        // 檢查是否已經有數量顯示
        if (element.inventoryCountText != null)
        {
            Debug.Log($"{element.name} 已經有數量顯示，跳過設置。");
            return false;
        }
        
        // 創建數量顯示的子物件
        GameObject countTextObj = new GameObject($"{element.name}_CountText");
        countTextObj.transform.SetParent(element.transform);
        
        // 設置 RectTransform
        RectTransform rectTransform = countTextObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = countTextOffset;
        rectTransform.sizeDelta = new Vector2(30, 20);
        
        // 添加 Text 元件
        Text countText = countTextObj.AddComponent<Text>();
        countText.text = "0";
        countText.font = textFont != null ? textFont : Resources.GetBuiltinResource<Font>("Arial.ttf");
        countText.fontSize = fontSize;
        countText.color = textColor;
        countText.alignment = TextAnchor.MiddleCenter;
        
        // 設置到 BaseElement
        element.inventoryCountText = countText;
        element.showInventoryCount = true;
        
        // 立即更新顯示
        element.UpdateInventoryCountDisplay();
        
        Debug.Log($"為 {element.name} 設置了數量顯示。");
        return true;
    }
    
    /// <summary>
    /// 清除所有元素的數量顯示（用於重新設置）
    /// </summary>
    public void ClearAllCountDisplays()
    {
        BaseElement[] elements = FindObjectsOfType<BaseElement>();
        int clearCount = 0;
        
        foreach (BaseElement element in elements)
        {
            if (element.inventoryCountText != null)
            {
                // 刪除數量顯示物件
                DestroyImmediate(element.inventoryCountText.gameObject);
                element.inventoryCountText = null;
                clearCount++;
            }
        }
        
        Debug.Log($"清除了 {clearCount} 個元素的數量顯示。");
    }
}