using UnityEngine;
using UnityEngine.SceneManagement; 

public class SunClickDetector : MonoBehaviour
{
    void OnMouseDown()
    {
        // 直接寫死要跳轉的場景名稱
        SceneManager.LoadScene("CraftingStudio");
        // Debug.Log("CLICK DETECTED on Spaceship!");
    }
}
