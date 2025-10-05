using UnityEngine;
using UnityEngine.SceneManagement; 

public class SunClickDetector : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("CraftingStudio");
    }
}
