using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClickCraft : MonoBehaviour
{
    [Header("Required Components")]
    public Pot pot;
    
    [Header("UI Feedback")]
    public Text feedbackText;
    public float feedbackDuration = 3f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;
    
    private GameObject lastCraftedPlanet;
    
    public void OnCraftButtonClicked()
    {
        CraftPlanet();
    }
    
    void OnMouseDown()
    {
        CraftPlanet();
    }
    
    private void CraftPlanet()
    {
        if (pot == null)
        {
            Debug.LogError("Pot is not assigned to ClickCraft!");
            return;
        }
        
        GameObject craftedPlanet = pot.TryCraft();
        
        if (craftedPlanet != null)
        {
            lastCraftedPlanet = craftedPlanet;
            OnCraftSuccess(craftedPlanet);
        }
        else
        {
            OnCraftFailed();
        }
    }
    
    private void OnCraftSuccess(GameObject planet)
    {
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }
        
        if (feedbackText != null)
        {
            string planetName = "Unknown";
            
            if (planet.GetComponent<Mercury>() != null) planetName = "Mercury (水星)";
            else if (planet.GetComponent<Venus>() != null) planetName = "Venus (金星)";
            else if (planet.GetComponent<Earth>() != null) planetName = "Earth (地球)";
            else if (planet.GetComponent<Mars>() != null) planetName = "Mars (火星)";
            else if (planet.GetComponent<Jupiter>() != null) planetName = "Jupiter (木星)";
            else if (planet.GetComponent<Saturn>() != null) planetName = "Saturn (土星)";
            else if (planet.GetComponent<Uranus>() != null) planetName = "Uranus (天王星)";
            else if (planet.GetComponent<Neptune>() != null) planetName = "Neptune (海王星)";
            else if (planet.GetComponent<Pluto>() != null) planetName = "Pluto (冥王星)";
            
            feedbackText.text = $"Successfully crafted: {planetName}!";
            CancelInvoke("ClearFeedback");
            Invoke("ClearFeedback", feedbackDuration);
        }
        
        Debug.Log($"Planet {planet.name} successfully crafted!");
    }
    
    private void OnCraftFailed()
    {
        if (audioSource != null && failSound != null)
        {
            audioSource.PlayOneShot(failSound);
        }
        
        if (feedbackText != null)
        {
            feedbackText.text = "Crafting failed! Check your elements.";
            CancelInvoke("ClearFeedback");
            Invoke("ClearFeedback", feedbackDuration);
        }
        
        Debug.Log("Crafting failed!");
        pot.ClearPot();
    }
    
    private void ClearFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }
    
    public GameObject GetLastCraftedPlanet()
    {
        return lastCraftedPlanet;
    }
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnCraftButtonClicked);
        
    }

    void Update()
    {
        
    }
}
