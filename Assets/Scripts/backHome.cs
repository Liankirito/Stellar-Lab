using UnityEngine;
using UnityEngine.SceneManagement; 

public class backHome : MonoBehaviour
{
    void OnMouseDown()
    {
        // 直接寫死要跳轉的場景名稱
        SceneManager.LoadScene("Index");
        // Debug.Log("CLICK DETECTED on Spaceship!");
    }
}
