using UnityEngine;
using UnityEngine.SceneManagement;

public class Explore : MonoBehaviour
{
    // 我們不再使用 OnMouseDown
    // 而是建立一個公開的函式，讓按鈕可以呼叫
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene("Resource Exploration");
    }
}