using UnityEngine;

/**
 * Base class for all chemical elements in the crafting system
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
            Debug.Log($"Adding {elementName} to pot");
            targetPot.AddElementToPot(elementName);
            
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
            Debug.LogWarning($"No target pot assigned for {elementName}!");
        }
    }
    
    protected virtual void OnElementAdded()
    {
        // Override in derived classes for custom behavior
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