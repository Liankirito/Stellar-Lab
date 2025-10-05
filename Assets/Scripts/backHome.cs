using UnityEngine;
using UnityEngine.SceneManagement; 

public class backHome : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("inital");
    }
}
