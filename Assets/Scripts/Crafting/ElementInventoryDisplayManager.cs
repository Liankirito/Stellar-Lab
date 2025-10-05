using System.Collections.Generic;
using UnityEngine;


public class ElementInventoryDisplayManager : MonoBehaviour
{
    [Header("Update Settings")]
    public float updateInterval = 0.5f; 
    
    private List<BaseElement> elements = new List<BaseElement>();
    private float lastUpdateTime = 0f;
    
    void Start()
    {
        
        RefreshElementsList();
        
        
        UpdateAllElementsDisplay();
    }
    
    void Update()
    {
        
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            UpdateAllElementsDisplay();
            lastUpdateTime = Time.time;
        }
    }
    

    public void RefreshElementsList()
    {
        elements.Clear();
        BaseElement[] foundElements = FindObjectsOfType<BaseElement>();
        elements.AddRange(foundElements);
        
        Debug.Log($"Found {elements.Count} elements in the scene");
    }
    

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
    

    public void ForceUpdateAllElementsDisplay()
    {
        UpdateAllElementsDisplay();
        lastUpdateTime = Time.time;
    }
}