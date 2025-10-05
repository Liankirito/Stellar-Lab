using UnityEngine;
using UnityEngine.UI;

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
    public Text inventoryCountText;
    public bool showInventoryCount = true;
    
    [Header("Visual States")]
    public bool changeColorWhenEmpty = true;
    public Color emptyColor = Color.gray;
    public Color availableColor = Color.white;
    private Image elementImage;
    
    protected virtual void Start()
    {
        if (targetPot == null)
        {
            targetPot = FindObjectOfType<Pot>();
        }
        
        if (string.IsNullOrEmpty(elementName))
        {
            elementName = GetType().Name;
        }
        
        elementImage = GetComponent<Image>();
        
        UpdateInventoryCountDisplay();
    }
    
    void OnMouseDown()
    {
        AddToPot();
    }
    
    public void OnElementClicked()
    {
        AddToPot();
    }
    
    protected virtual void AddToPot()
    {
        if (targetPot != null)
        {
            if (InventoryManager.GetResourceCount(elementName) > 0)
            {
                Debug.Log($"Adding {elementName} to pot");
                targetPot.AddElementToPot(elementName);
                
                InventoryManager.UseResource(elementName, 1);
                
                if (audioSource != null && addSound != null)
                {
                    audioSource.PlayOneShot(addSound);
                }
                
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
        UpdateInventoryCountDisplay();
    }
    
    public void UpdateInventoryCountDisplay()
    {
        if (!string.IsNullOrEmpty(elementName))
        {
            int count = InventoryManager.GetResourceCount(elementName);
            
            if (inventoryCountText != null && showInventoryCount)
            {
                inventoryCountText.text = count.ToString();
                
                if (changeColorWhenEmpty)
                {
                    inventoryCountText.color = count > 0 ? availableColor : emptyColor;
                }
            }
            
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
        
        float timer = 0;
        while (timer < scaleDuration / 2)
        {
            timer += Time.deltaTime;
            float progress = timer / (scaleDuration / 2);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }
        
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