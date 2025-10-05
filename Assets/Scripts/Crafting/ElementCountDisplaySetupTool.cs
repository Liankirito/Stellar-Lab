using UnityEngine;
using UnityEngine.UI;

public class ElementCountDisplaySetupTool : MonoBehaviour
{
    [Header("Auto Setup Settings")]
    public Font textFont;
    public int fontSize = 14;
    public Color textColor = Color.white;
    public Vector2 countTextOffset = new Vector2(15, -15);
    
    [Header("Actions")]
    [Space(10)]
    public bool setupAllElements = false;
    
    void Update()
    {
        if (setupAllElements)
        {
            setupAllElements = false;
            SetupAllElementsCountDisplay();
        }
    }
    
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
    
    private bool SetupElementCountDisplay(BaseElement element)
    {
        if (element == null) return false;
        
        if (element.inventoryCountText != null)
        {
            Debug.Log($"{element.name} 已經有數量顯示，跳過設置。");
            return false;
        }
        
        GameObject countTextObj = new GameObject($"{element.name}_CountText");
        countTextObj.transform.SetParent(element.transform);
        
        RectTransform rectTransform = countTextObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = countTextOffset;
        rectTransform.sizeDelta = new Vector2(30, 20);
        
        Text countText = countTextObj.AddComponent<Text>();
        countText.text = "0";
        countText.font = textFont != null ? textFont : Resources.GetBuiltinResource<Font>("Arial.ttf");
        countText.fontSize = fontSize;
        countText.color = textColor;
        countText.alignment = TextAnchor.MiddleCenter;
        
        element.inventoryCountText = countText;
        element.showInventoryCount = true;
        
        element.UpdateInventoryCountDisplay();
        
        Debug.Log($"為 {element.name} 設置了數量顯示。");
        return true;
    }
    
    public void ClearAllCountDisplays()
    {
        BaseElement[] elements = FindObjectsOfType<BaseElement>();
        int clearCount = 0;
        
        foreach (BaseElement element in elements)
        {
            if (element.inventoryCountText != null)
            {
                DestroyImmediate(element.inventoryCountText.gameObject);
                element.inventoryCountText = null;
                clearCount++;
            }
        }
        
        Debug.Log($"清除了 {clearCount} 個元素的數量顯示。");
    }
}