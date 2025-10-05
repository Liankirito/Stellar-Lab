using UnityEngine;
using UnityEngine.SceneManagement;

public class Explore : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene("Resource Exploration");
    }
}