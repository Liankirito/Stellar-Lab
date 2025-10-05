using UnityEngine;
using UnityEngine.UI;

/**
 * Base class for all chemical elements in the crafting system(AI-assisted)
 */
public abstract class BaseElement : MonoBehaviour
{
    [Header("Element Settings")]
    public string elementName;
    
    [Header("Pot Reference")]
    public Pot targetPot;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip addSound;
    
    [Header("Visual Feedback")]
    public bool scaleOnClick = true;
    public float scaleAmount = 1.2f;
    public float scaleDuration = 0.2f;
    
    [Header("Inventory Display")]
    public Text inventoryCountText; // 顯示背包中素材數量的文字元件
    public bool showInventoryCount = true; // 是否顯示背包數量
    
    [Header("Visual States")]
    public bool changeColorWhenEmpty = true; // 當素材為空時是否改變顏色
    public Color emptyColor = Color.gray; // 素材為空時的顏色
    public Color availableColor = Color.white; // 有素材時的顏色
    private Image elementImage; // 元素的圖片元件
    
    protected virtual void Start()
    {
        // If no pot is assigned, try to find one in the scene
        if (targetPot == null)
        {
            targetPot = FindObjectOfType<Pot>();
        }
        
        // Set element name if not already set
        if (string.IsNullOrEmpty(elementName))
        {
            elementName = GetType().Name;
        }
        
        // Get image component for visual feedback
        elementImage = GetComponent<Image>();
        
        // Initialize inventory count display
        UpdateInventoryCountDisplay();
    }
    
    // For mouse click
    void OnMouseDown()
    {
        AddToPot();
    }
    
    // For UI Button
    public void OnElementClicked()
    {
        AddToPot();
    }
    
    protected virtual void AddToPot()
    {
        if (targetPot != null)
        {
            // 檢查背包中是否有足夠的素材
            if (InventoryManager.GetResourceCount(elementName) > 0)
            {
                Debug.Log($"Adding {elementName} to pot");
                targetPot.AddElementToPot(elementName);
                
                // 從背包中消耗一個素材
                InventoryManager.UseResource(elementName, 1);
                
                // Play sound effect
                if (audioSource != null && addSound != null)
                {
                    audioSource.PlayOneShot(addSound);
                }
                
                // Visual feedback
                if (scaleOnClick)
                {
                    StartCoroutine(ScaleEffect());
                }
                
                Debug.Log($"Added {elementName} to pot");
                OnElementAdded();
            }
            else
            {
                Debug.LogWarning($"No {elementName} available in inventory!");
            }
        }
        else
        {
            Debug.LogWarning($"No target pot assigned for {elementName}!");
        }
    }
    
    protected virtual void OnElementAdded()
    {
        // Override in derived classes for custom behavior
        // Update inventory count display after adding element
        UpdateInventoryCountDisplay();
    }
    
    /// <summary>
    /// 更新背包數量顯示
    /// </summary>
    public void UpdateInventoryCountDisplay()
    {
        if (!string.IsNullOrEmpty(elementName))
        {
            int count = InventoryManager.GetResourceCount(elementName);
            
            // 更新數量文字
            if (inventoryCountText != null && showInventoryCount)
            {
                inventoryCountText.text = count.ToString();
                
                // 根據數量改變文字顏色
                if (changeColorWhenEmpty)
                {
                    inventoryCountText.color = count > 0 ? availableColor : emptyColor;
                }
            }
            
            // 更新元素圖片顏色
            if (elementImage != null && changeColorWhenEmpty)
            {
                elementImage.color = count > 0 ? availableColor : emptyColor;
            }
        }
    }
    
    private System.Collections.IEnumerator ScaleEffect()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * scaleAmount;
        
        // Scale up
        float timer = 0;
        while (timer < scaleDuration / 2)
        {
            timer += Time.deltaTime;
            float progress = timer / (scaleDuration / 2);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }
        
        // Scale down
        timer = 0;
        while (timer < scaleDuration / 2)
        {
            timer += Time.deltaTime;
            float progress = timer / (scaleDuration / 2);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            yield return null;
        }
        
        transform.localScale = originalScale;
    }
    

}