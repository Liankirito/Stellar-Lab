using UnityEngine;
using UnityEngine.SceneManagement; 

public class Resource : MonoBehaviour
{
    void OnMouseDown()
    {
        // 直接寫死要跳轉的場景名稱
        SceneManager.LoadScene("Resource Exploration");
        // Debug.Log("CLICK DETECTED on Spaceship!");
    }
}
