using System.Collections.Generic;
using UnityEngine;

/**
 * 管理工作台上所有元素的背包數量顯示
 */
public class ElementInventoryDisplayManager : MonoBehaviour
{
    [Header("Update Settings")]
    public float updateInterval = 0.5f; // 更新間隔（秒）
    
    private List<BaseElement> elements = new List<BaseElement>();
    private float lastUpdateTime = 0f;
    
    void Start()
    {
        // 找到場景中所有的 BaseElement
        RefreshElementsList();
        
        // 初始更新所有元素的數量顯示
        UpdateAllElementsDisplay();
    }
    
    void Update()
    {
        // 定期更新所有元素的數量顯示
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            UpdateAllElementsDisplay();
            lastUpdateTime = Time.time;
        }
    }
    
    /// <summary>
    /// 重新搜尋場景中的所有元素
    /// </summary>
    public void RefreshElementsList()
    {
        elements.Clear();
        BaseElement[] foundElements = FindObjectsOfType<BaseElement>();
        elements.AddRange(foundElements);
        
        Debug.Log($"Found {elements.Count} elements in the scene");
    }
    
    /// <summary>
    /// 更新所有元素的數量顯示
    /// </summary>
    public void UpdateAllElementsDisplay()
    {
        foreach (BaseElement element in elements)
        {
            if (element != null)
            {
                element.UpdateInventoryCountDisplay();
            }
        }
    }
    
    /// <summary>
    /// 立即更新所有元素的數量顯示（當背包內容發生變化時呼叫）
    /// </summary>
    public void ForceUpdateAllElementsDisplay()
    {
        UpdateAllElementsDisplay();
        lastUpdateTime = Time.time;
    }
}