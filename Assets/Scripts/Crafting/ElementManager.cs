using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementManager : MonoBehaviour
{
    [Header("Added Elements")]
    public List<string> addedElements = new List<string>();
    
    [Header("UI Elements")]
    public Text elementsDisplayText;
    
    [Header("Debug")]
    public bool showDebugLog = true;
    
    public void AddElement(string elementName)
    {
        if (!string.IsNullOrEmpty(elementName))
        {
            addedElements.Add(elementName);
            if (showDebugLog)
                Debug.Log($"Added element: {elementName}. Total elements: {addedElements.Count}");
            
            UpdateElementsDisplay();
        }
    }
    
    public void RemoveElement(string elementName)
    {
        if (addedElements.Remove(elementName))
        {
            if (showDebugLog)
                Debug.Log($"Removed element: {elementName}. Total elements: {addedElements.Count}");
            
            UpdateElementsDisplay();
        }
    }
    
    public void ClearAllElements()
    {
        addedElements.Clear();
        if (showDebugLog)
            Debug.Log("Cleared all elements");
        
        UpdateElementsDisplay();
    }
    
    public List<string> GetAddedElements()
    {
        return new List<string>(addedElements);
    }
    
    public int GetElementCount()
    {
        return addedElements.Count;
    }
    
    public bool HasElement(string elementName)
    {
        return addedElements.Contains(elementName);
    }
    
    private void UpdateElementsDisplay()
    {
        if (elementsDisplayText != null)
        {
            elementsDisplayText.text = "Elements: " + string.Join(", ", addedElements);
        }
    }
    
    void Start()
    {
        UpdateElementsDisplay();
    }

    void Update()
    {
        
    }
}
