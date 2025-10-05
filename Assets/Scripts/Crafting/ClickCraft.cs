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
    public Text feedbackText; // 可選：顯示合成結果的文本
    public float feedbackDuration = 3f; // 反饋文本顯示時間
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;
    
    private GameObject lastCraftedPlanet;
    
    // 用於 Button 的 OnClick 事件
    public void OnCraftButtonClicked()
    {
        CraftPlanet();
    }
    
    // 也可以用於滑鼠點擊事件
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
        // 播放成功音效
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }
        
        // 顯示成功消息
        if (feedbackText != null)
        {
            string planetName = "Unknown";
            
            // 嘗試獲取星球名稱
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
        // 播放失敗音效
        if (audioSource != null && failSound != null)
        {
            audioSource.PlayOneShot(failSound);
        }
        
        // 顯示失敗消息
        if (feedbackText != null)
        {
            feedbackText.text = "Crafting failed! Check your elements.";
            CancelInvoke("ClearFeedback");
            Invoke("ClearFeedback", feedbackDuration);
        }
        
        Debug.Log("Crafting failed!");
        // 清空 pot
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnCraftButtonClicked);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
